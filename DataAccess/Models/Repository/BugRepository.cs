using DataAccess.Models.IRepository;
using DataAccess.Models.StronglyTypes;
using Lucene.Net.Store;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace DataAccess.Models.Repository
{
    public class BugRepository : AbstractCreateRepository, IBugRepository
    {
        private readonly BugTriageContext context;

        public BugRepository(BugTriageContext context) : base(context)
        {
            this.context = context;
        }
        public object SaveBugProfile(BugModel bug)
        {
            Bug existingBug = context.Bugs.FirstOrDefault(x => x.id == bug.id);
            if(existingBug!=null)return false;

            Bug bugData = bug;
            int productId = CreateInsatntProductProfile(bug.ProductName);
            int componentId = CreateInsatntComponentProfile(bug.ComponentName);
            int hardwareId = CreateInsatntHardwareProfile(bug.HardwareName);
            int developerid = CreateInsatntDeveloperProfile(bug.AssigneeName);
            if (productId == 0 || componentId == 0 || hardwareId == 0) return false;
            context.ProductDeveloperMappings.Add(new ProductDeveloperMapping { product_id = productId, developer_id = developerid });
            context.ComponentDeveloperMappings.Add(new ComponentDeveloperMapping { component_id = componentId, developer_id = developerid });
            context.HardwareDeveloperMappings.Add(new HardwareDeveloperMapping { hardware_id = hardwareId, developer_id = developerid });
            context.SaveChanges();

            bugData.Product = productId;
            bugData.Component = componentId;
            bugData.Hardware = hardwareId;
            bugData.Assignee = developerid;
            bugData.Changed = bug.Changed;
            context.Bugs.Add(bugData);
            context.SaveChanges();

            context.bug_developer_mappings.Add(new BugDeveloperMapping { bug_id = bug.BugId, developer_id = developerid });
            context.SaveChanges();
            return true;
        }

        public object GetBugs(int startNo, int lengthNo, string search, string status)
        {
            int id = 0; 
            int.TryParse(search,out id);
            var data =(from bugs in context.Bugs
                    join pro in context.Products on bugs.Product equals pro.ProductId
                    join hw in context.Hardwares on bugs.Hardware equals hw.hardware_id
                    join comp in context.Components on bugs.Component equals comp.ComponentId
                    select new BugModel
                    {
                        BugId = bugs.BugId,
                        Status = bugs.Status,
                        ProductName = pro.ProductName,
                        ComponentName = comp.ComponentName,
                        Severity = bugs.Severity,
                        Priority = bugs.Priority,
                        Summary = bugs.Summary,
                        HardwareName = hw.hardware_name,
                        Description = bugs.Description,
                        KeyWords = bugs.KeyWords,
                        AssigneeName = string.Join(',', ((from bugdev in context.bug_developer_mappings.Where(x => x.bug_id == bugs.BugId)
                                                          join dev in context.Developer on bugdev.developer_id equals dev.DeveloperId
                                                          select dev.DeveloperName).ToArray())),
                        id = bugs.id,
                        Changed = bugs.Changed
                    });
            if (status == "New") {
                data = data.Where(x => x.Status == "new");
            }
            else {
                data = data.Where(x => x.Status != "new");
            }
             
            if (id != 0) {
                data = data.Where(x => x.id == id || x.BugId==id);
            }
            if (lengthNo > 0) {
                data = data.Skip(startNo).Take(lengthNo);
            }

            return data;

        }


        public int ExecuteSql(List<Bug> bugs)
        {
            int row = 0;
            bugs.ForEach(bug =>
            {
                int developerId = bug.Assignee;
                string[] skills = bug.KeyWords.Split(',');
                foreach (string skill in skills) {
                    Skills isExistSkill = context.Skills.FirstOrDefault(x => x.Skill == skill);
                    if (isExistSkill == null)
                    {
                        context.Add(new Skills() { Skill = skill, Developers = developerId.ToString() });
                    }
                    else {
                        string existingDevelopers = isExistSkill.Developers;
                        isExistSkill.Developers = existingDevelopers + "," + developerId;
                    }
                    context.SaveChanges();

                }


           

                if (bug.Assignee != 0 && bug.Product != 0) {
                    context.ProductDeveloperMappings.Add(new ProductDeveloperMapping { developer_id = bug.Assignee, product_id = bug.Product });
                }
                if (bug.Assignee != 0 && bug.Component != 0) {
                    context.ComponentDeveloperMappings.Add(new ComponentDeveloperMapping { developer_id = bug.Assignee, component_id = bug.Component });
                }
                if (bug.Assignee != 0 && bug.Hardware != 0) {
                    context.HardwareDeveloperMappings.Add(new HardwareDeveloperMapping { developer_id = bug.Assignee, hardware_id = bug.Hardware });
                }


                context.SaveChanges();


                if (bug.Assignee != 0) {
                    Developer developer = context.Developer.Find(developerId);
                    string developerSkils = developer.Skills + ((developer.Skills == null) ? "" + bug.KeyWords : "," + bug.KeyWords);
                    string distinctSkill = string.Join(',', developerSkils.Split(',').Distinct());
                    developer.Skills = distinctSkill;
                    context.SaveChanges();
                }


             

                var res = context.Bugs.Add(bug);
                context.SaveChanges();
                if (bug.Assignee != 0)
                {
                    context.bug_developer_mappings.Add(new BugDeveloperMapping { bug_id = bug.BugId, developer_id = bug.Assignee });
                }
                row++;
            });
            return row;
        }

        public object Delete(string source)
        {
            try
            {
                IEnumerable<Bug> bugList = (source!="All")?context.Bugs.Where(x => x.Source == source): context.Bugs;

                int[] bugs = bugList.Select(x => x.BugId).ToArray();
                string bugIds = string.Join(',', bugs);

                if (bugs != null && bugs.Length != 0) {
                    string deleteDevelopersMapping = "DELETE FROM bug_developer_mappings WHERE bug_id IN(" + bugIds + ")";
                    string deleteDevelopers = "DELETE FROM developer WHERE developer_id IN(SELECT developer_id FROM bug_developer_mappings WHERE bug_id IN (" + bugIds + "))";
                    context.Database.ExecuteSqlCommand(deleteDevelopers);
                    context.Database.ExecuteSqlCommand(deleteDevelopersMapping);
                   
                }

                foreach (Bug item in bugList)
                {
                    context.Bugs.Remove(item);

                }
                context.SaveChanges();

                return new { output = "success", msg = "Bug Profiles Deleted Successfully" };
            }
            catch (Exception e)
            {
                return new { output = "error", msg = e.Message };
                throw e;
            }

        }

        public Confirmation SaveSearchProfileConfig(ProfileMatchConfig matchConfig)
        {
            try
            {
                ProfileMatchConfig previousConfig = context.profile_match_config.Find(matchConfig.profile_match_config_id);
                if (previousConfig != null)
                {
                    previousConfig.no_of_sed = matchConfig.no_of_sed;
                    previousConfig.no_of_ned = matchConfig.no_of_ned;
                    previousConfig.no_of_fd = matchConfig.no_of_fd;
                    context.SaveChanges();
                }
                else {
                    previousConfig = new ProfileMatchConfig();
                    previousConfig.severity = matchConfig.severity;
                    previousConfig.no_of_sed = matchConfig.no_of_sed;
                    previousConfig.no_of_ned = matchConfig.no_of_ned;
                    previousConfig.no_of_fd = matchConfig.no_of_fd;
                    context.profile_match_config.Add(previousConfig);
                    context.SaveChanges();
                }
               
                return new Confirmation { is_succeed = true, msg = "Config Saved" };

            }
            catch (Exception e)
            {

                return new Confirmation { is_succeed=false,msg=e.Message};
            }

        }

        public Confirmation GetSearchProfileConfig(string severity)
        {
            try
            {
                
                ProfileMatchConfig config= context.profile_match_config.FirstOrDefault(x => x.severity == severity);
                int row = (config==null) ?0:1;
                return new Confirmation {is_succeed=true,output=config, msg= row+" Result Found"};
            }
            catch (Exception e)
            {

                return new Confirmation { is_succeed = true, msg=e.Message }; 
            }
        }

        public int GetBugCount()
        {
            return context.Bugs.Count();
        }
    }
}
