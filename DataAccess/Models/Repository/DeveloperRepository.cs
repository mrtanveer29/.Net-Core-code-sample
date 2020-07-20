using DataAccess.Models.IRepository;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models.StronglyTypes;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

namespace DataAccess.Models.Repository
{
    public class DeveloperRepository : AbstractCreateRepository, IDeveloperRepository
    {
        private BugTriageContext context;
        public DeveloperRepository(BugTriageContext context):base(context)
        {
            this.context = context;
        }

        public object AddDeveloper(DeveloperModel developer)
        {
            string[] products = developer.ProductName?.Split(',');
            string[] components = developer.ComponentName?.Split(',');
            string[] hardwares = developer.HardwareName?.Split(',');
            int developerid = CreateInsatntDeveloperProfile(developer.DeveloperName);
            if (developerid == 0) return 0;

         
            foreach (string item in products??new string[0])
            {
                int productId = CreateInsatntProductProfile(item);
                if (productId > 0)
                {
                    context.ProductDeveloperMappings.Add(new ProductDeveloperMapping { product_id = productId, developer_id = developerid });
                }
            }

            foreach (string item in components ?? new string[0])
            {
                int componentId = CreateInsatntComponentProfile(item);

                if (componentId > 0)
                {
                    context.ComponentDeveloperMappings.Add(new ComponentDeveloperMapping { component_id = componentId, developer_id = developerid });
                }
            }

            foreach (string item in hardwares ?? new string[0])
            {
                int hardwareId = CreateInsatntHardwareProfile(item);


                if (hardwareId > 0)
                {
                    context.HardwareDeveloperMappings.Add(new HardwareDeveloperMapping { hardware_id = hardwareId, developer_id = developerid });
                }
            }



            context.SaveChanges();




            string []skills = developer.Skills.Split(',');
            foreach (string skill in skills ?? new string[0])
            {
                IEnumerable<Skills> prevSkills = context.Skills.Where(x => x.Skill == skill);
                
                if (!prevSkills.Any())
                {
                    context.Skills.Add(new Skills { Skill = skill, Developers = developer.DeveloperId.ToString() });
                    context.SaveChanges();
                }
               
            }

            Developer exdev = context.Developer.Find(developerid);
            exdev.PreviousWorks = developer.PreviousWorks;
            exdev.DeveloperTypeId = developer.DeveloperTypeId;
            exdev.Skills = developer.Skills;
            return context.SaveChanges();

        }

        public object DeleteDeveloper(int id, int type)
        {
            try
            {
                if (id > 0)
                {
                    Developer developer = context.Developer.Find(id);
                    string name = developer.DeveloperName;
                    context.Developer.Remove(developer);
                    context.SaveChanges();
                    return new { output = "success", msg = name + " Deleted!" };
                }
                else if (type > 0)
                {
                    context.Developer.RemoveRange(context.Developer.Where(x => x.DeveloperTypeId == type));
                    context.SaveChanges();
                    return new { output = "success", msg = "Developers Deleted!" };
                }
                else
                {
                    return new { output = "error", msg = "Invalid Options Passed" };
                }

            }
            catch (Exception e)
            {

                return new { output = "error", msg = e.Message };
            }


        }

        public int ExecuteSql(List<DeveloperModel> queries)
        {
            int row = 0;
            int count = 0;
            foreach (DeveloperModel developer in queries)
            {
                AddDeveloper(developer);
                count++;
            }
          
            return count;
        }

        public ProfileMatchConfig GetConfig(string v)
        {
            return context.profile_match_config.FirstOrDefault(x => x.severity == v);
        }

        public object GetCurrentDataCount()
        {
            var developers = (from dev in context.Developer select dev).GroupBy(x => x.DeveloperTypeId).Select(s => new SummaryModel { DeveloperType=s.Key,Count=s.Select(x=>x.DeveloperName).Count() });

            var products = context.Products.Count();
            var components = context.Components.Count();
            var bugs = context.Bugs.Count();

            return new {
            sed= developers.FirstOrDefault(x=>x.DeveloperType==1)?.Count,
            ned= developers.FirstOrDefault(x=>x.DeveloperType==2)?.Count,
            fd= developers.FirstOrDefault(x=>x.DeveloperType==3)?.Count,
            bugs,products,components
            };

        }

        public object GetDeveloper(int developerId)
        {
            DeveloperModel developer = (from d in context.Developer.Where(x => x.DeveloperId == developerId)
                                        join dt in context.DeveloperTypes on d.DeveloperTypeId equals dt.DeveloperTypeId
                                        select new DeveloperModel
                                        {
                                            DeveloperId = d.DeveloperId,
                                            DeveloperName = d.DeveloperName,
                                            DeveloperTypeName = dt.DeveloperTypeName,
                                            Skills = d.Skills ?? "NA",
                                            PreviousWorks=d.PreviousWorks,
                                            current_workload=d.current_workload??0
                                        }).FirstOrDefault();
         

            var bugDetails = (from dm in context.bug_developer_mappings.Where(x => x.developer_id == developerId)
                             
                                    join bug in context.Bugs on dm.bug_id equals bug.BugId
                                    select bug.Status).ToList();
            var productDetails = (from d in context.Developer.Where(x => x.DeveloperId == developerId)
                                  join pm in context.ProductDeveloperMappings on d.DeveloperId equals pm.developer_id
                                  join pro in context.Products on pm.product_id equals pro.ProductId
                                  select new
                                  {
                                      Product = pm.product_id,
                                      pm.product_developer_mapping_id,
                                      ProductName = pro.ProductName,
                                  });
            var componentDetails = (from d in context.Developer.Where(x => x.DeveloperId == developerId)
                                    join cm in context.ComponentDeveloperMappings on d.DeveloperId equals cm.developer_id
                                    join comp in context.Components on cm.component_id equals comp.ComponentId
                                    select new
                                    {
                                        cm.component_developer_mapping_id,
                                        ComponentName = comp.ComponentName,
                                        Component = cm.component_id,
                                    });
            var hardwareDetails = (from d in context.Developer.Where(x => x.DeveloperId == developerId)
                                   join hm in context.HardwareDeveloperMappings on d.DeveloperId equals hm.developer_id
                                   join hw in context.Hardwares on hm.hardware_id equals hw.hardware_id
                                   select new
                                   {
                                       Hardware = hm.hardware_id,
                                       HardwareName = hw.hardware_name,
                                   });
            List<ProductModel> productsWorked = productDetails.GroupBy(x => x.Product).Select(g => new ProductModel
            {
                ProductId = g.Key,
                ProductName = g.Max(x => x.ProductName),
                count = g.Count()
            }).ToList();
            List<ComponentModel> componentsWorked = componentDetails.GroupBy(x => x.Component).Select(g => new ComponentModel
            {
                ComponentId = g.Key,
                ComponentName = g.Max(x => x.ComponentName),
                count = g.Count()
            }).ToList();
            List<HardwareModel> hardwaresWorked = hardwareDetails.GroupBy(x => x.Hardware).Select(g => new HardwareModel
            {
                hardware_id = g.Key,
                hardware_name = g.Max(x => x.HardwareName),
                count = g.Count()
            }).ToList();

            developer.ProductsWorked = productsWorked;
            developer.ComponentsWorked = componentsWorked;
            developer.HardwaresWorked = hardwaresWorked;

            if (!bugDetails.Any())
            {
                return developer;
            }
            bool hasAny = bugDetails.Where(x => x != "New").Any();

        
            developer.SolveCount = (hasAny) ? bugDetails.Where(x => x != "New").Count() : 0;
            return developer;
        }

        public IEnumerable<DeveloperModel> GetDevelopers(int type)
        {
            var developers = (from d in context.Developer
                              join dt in context.DeveloperTypes on d.DeveloperTypeId equals dt.DeveloperTypeId
                              select new DeveloperModel
                              {
                                  DeveloperId = d.DeveloperId,
                                  DeveloperName = d.DeveloperName,
                                  DeveloperTypeId = d.DeveloperTypeId,
                                  DeveloperTypeName = dt.DeveloperTypeName,
                                  Skills = d.Skills ?? "NA"
                              });
            if (type > 0)
            {
                return developers.Where(x => x.DeveloperTypeId == type);
            }
            return developers;
        }

        public object GetDevelopersForIndex()
        {
            var developers = (from dev in context.Developer.Where(x => (x.is_indexed ?? false) == false)
                                
                              select new DeveloperModel
                              {
                                  DeveloperId = dev.DeveloperId,
                                  DeveloperTypeId = dev.DeveloperTypeId,

                                  DeveloperName = dev.DeveloperName,
                                  Skills = dev.Skills,
                                  ComponentName = string.Join(',', (from cd in context.ComponentDeveloperMappings.Where(x=>x.developer_id==dev.DeveloperId)
                                                                    join com in context.Components on cd.component_id equals com.ComponentId
                                                                    select (com.ComponentName??"").ToLower()).ToArray()),
                                  ProductName = string.Join(',', (from pd in context.ProductDeveloperMappings.Where(x => x.developer_id == dev.DeveloperId)
                                                                  join pro in context.Products on pd.product_id equals pro.ProductId
                                                                  select pro.ProductName??"").ToArray())
                              });
            return developers;
        }

        public IEnumerable<DeveloperTypes> GetDeveloperTypes()
        {
            return context.DeveloperTypes;
        }

        public object GetSkills()
        {
            return from sk in context.Skills select sk.Skill;
        }

        public void MarkAsIndexed(IEnumerable<DeveloperModel> developers)
        {
            foreach (DeveloperModel item in developers)
            {
                Developer dep = context.Developer.Find(item.DeveloperId);
                dep.is_indexed = true;
            }
            context.SaveChanges();
        }
    }
}
