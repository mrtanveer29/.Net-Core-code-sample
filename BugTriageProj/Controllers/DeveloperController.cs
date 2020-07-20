using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models.IRepository;
using BugTriage.ViewModels;
using DataAccess.Models;
using DataAccess.Models.StronglyTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using BugTriage.Utils;
using System.Text.RegularExpressions;
using System.Reflection;
using BugTriage.Search;
using BugTriage.Search.Searchables;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using NPOI.OpenXmlFormats.Dml.Chart;

namespace BugTriage.Controllers
{
    public class DeveloperController : Controller
    {
        public IDeveloperRepository developerRepository;
        private readonly IHostingEnvironment environment;
        private readonly ISearchManager searchManager;

        public DeveloperController(IDeveloperRepository developerRepository, IHostingEnvironment environment, ISearchManager searchManager)
        {
            this.developerRepository = developerRepository;
            this.environment = environment;
            this.searchManager = searchManager;
        }
     
       
        public ViewResult FreshDevelopers()
        {

            return View();
        }
        public ViewResult NewExperiencedDevelopers()
        {

            return View();
        }
        public ViewResult SiniorExperiencedDevelopers()
        {

            return View();
        }
        [HttpGet]
        public IActionResult Profile(int id)
        {
           
            DeveloperModel developer =(DeveloperModel) developerRepository.GetDeveloper(id);

            DeveloperViewModel developerViewModel = new DeveloperViewModel
            {
                DeveloperId = developer.DeveloperId,
                DeveloperName = developer.DeveloperName,
                DeveloperTypeName = developer.DeveloperTypeName,
                ProductsWorked = developer.ProductsWorked,
                ComponentsWorked = developer.ComponentsWorked,
                HardwaresWorked = developer.HardwaresWorked,
                PreviousWorks = developer.PreviousWorks,
                Skills = developer.Skills,
                SolveCount = developer.SolveCount,
                //current_workload=developer.current_workload??0

            };
            return View(developerViewModel);
        }
        [HttpGet]
        public ViewResult Create()
        {
           
            IEnumerable<DeveloperTypes> developerTypes = developerRepository.GetDeveloperTypes();
            var result = developerRepository.GetSkills();
            ViewBag.skills = Json(result);
            ViewBag.developerTypes = developerTypes;
            return View();
        }
      

        [HttpPost]
        public IActionResult Create(DeveloperViewModel developer)
        {
            string skillset = "";
            List<JsonValue> skills = JsonConvert.DeserializeObject<List<JsonValue>>(developer.Skills);
            foreach (JsonValue skill in skills)
            {
                string processedSkill = TextFormaterTool.DataPreprocess(skill.value);
                if (processedSkill == "" || processedSkill == null) continue;
                skillset += (processedSkill + ",");
            }
           
            DeveloperModel dev = new DeveloperModel
            {
                DeveloperName = developer.DeveloperName.Trim(),
                DeveloperTypeId = developer.DeveloperType,
                Skills= skillset,
                ProductName=developer.Products,
                ComponentName=developer.Components,
                HardwareName=developer.Platforms,
                PreviousWorks = developer.PreviousWorks,
                //current_workload=0

            };
            int developers = (int)developerRepository.AddDeveloper(dev);

            if (developer.DeveloperType==1)
            {
                return RedirectToAction("SiniorExperiencedDevelopers");
            }
            else if (developer.DeveloperType == 2)
            {
                return RedirectToAction("NewExperiencedDevelopers");
            }
            else
            {
                return RedirectToAction("FreshDevelopers");
            }
            
        }
        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }
        #region API Methods
        [HttpPost]
        public IActionResult GetDevelopers(int type=0)
        {
            IEnumerable<DeveloperModel> developers = developerRepository.GetDevelopers(type);
            return Json(developers);
        }
        [HttpDelete]
        public IActionResult DeleteDeveloper(int id=0,int type=0)
        {
           var  result = developerRepository.DeleteDeveloper(id, type);
            return Json(result);
        }
        public JsonResult GetSkills()
        {
            var result = developerRepository.GetSkills();
            return Json(result);
        }
        public JsonResult GetCurrentDataCount()
        {
            var result = developerRepository.GetCurrentDataCount();
            return Json(result);
        }
        [HttpGet]
        public IActionResult CreateIndex()
        {
            IEnumerable<DeveloperModel> developers = (IEnumerable<DeveloperModel>)developerRepository.GetDevelopersForIndex();
            List<Searchable> searchables = new List<Searchable>();
            foreach (DeveloperModel item in developers)
            {
                SearchableDeveloper searchableDeveloper = new SearchableDeveloper(item);
                searchables.Add(searchableDeveloper);
            }
          
            
            searchManager.AddToIndex(searchables.ToArray());
            //developerRepository.MarkAsIndexed(developers);
        
            return Json("success");
        }  
        [HttpGet]
        public IActionResult GetDevelopersForIndex()
        {
        
            return Json(developerRepository.GetDevelopersForIndex());
        }
        public List<DeveloperModel> GetDevelopersFromExcel(IFormFile file)
        {
            ExcelUpload<DeveloperModel> excel = new ExcelUpload<DeveloperModel>();
            string folderName = "Upload";
            string webRootPath = environment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            string[] allowedColumns = { "name", "skills", "keywords", "previousworks", "profilelink", "previouswork", "product", "component", "hardware" };

            return excel.ImportFromExcel(file, newPath, allowedColumns, (string colName, string colValue, DeveloperModel developer) => {

                if (colName == "name")
                {

                    developer.DeveloperName = colValue;
                   
                }
                if (colName == "product")
                {

                    developer.ProductName = colValue;
                   
                }
                if (colName == "component")
                {
                    developer.ComponentName = colValue;
                    
                }
                if (colName == "hardware")
                {

                    developer.HardwareName = colValue;
                  
                }
                if (colName == "previousworks" || colName == "previouswork" || colName == "profilelink")
                {

                    developer.PreviousWorks = colValue;
                   
                }
                if (colName == "keywords" || colName == "skills")
                {
                    string[] skills = colValue.Split(',');
                    string processedSkill = "";
                    foreach (var skill in skills)
                    {
                        var keyword = TextFormaterTool.DataPreprocess(skill);
                        if (keyword == null || keyword == "") continue;
                        processedSkill += keyword + ",";
                    }

                    developer.Skills = processedSkill.TrimEnd(',');
                    
                }
             


            });


        }
        public List<BugModel> GetBugsFromExcel(IFormFile file)
        {
            ExcelUpload<BugModel> excel = new ExcelUpload<BugModel>();
            string folderName = "Upload";
            string webRootPath = environment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            string[] allowedColumns =
                             {
                                    "product", "component", "severity", "priority", "summary",
                                    "description", "hardware", "assignee", "status", "resolution", "id", "changed"
                             };
            return excel.ImportFromExcel(file, newPath ,allowedColumns,(string coulumnName, string columnValue,BugModel bug) => {
            if (coulumnName == "assignee")
            {
                bug.AssigneeName = columnValue;
            }
            if ( coulumnName == "product")
            {
                bug.ProductName = columnValue;
            
            }
            if ( coulumnName == "component")
            {
                bug.ComponentName = columnValue;

            }
            if (coulumnName == "hardware")
            {
                bug.HardwareName = columnValue;
               
            }
            if (coulumnName == "severity")
            {
                string severityFromExcel = columnValue.Trim().ToLower();
                bug.SeverityTitle = severityFromExcel;
                
            }
            if (coulumnName == "priority")
            {

                string priorityFromExcel =columnValue.Trim().ToLower();
                bug.PriorityTitle = priorityFromExcel;
               
            }
            if (coulumnName == "summary" ||coulumnName == "description")
            {


                string colValue =
                    columnValue.Replace("\'", String.Empty)
                        .Replace("{", string.Empty)
                        .Replace("}", string.Empty)
                        .Replace("\"", String.Empty);

                var keyword = TextFormaterTool.DataPreprocess(colValue);
                    bug.KeyWords = bug.KeyWords + keyword;
            }


            
       
            });

           
        }
        private string GetSaverityForSearch(string item) {
            string[] severityList =
                         {
                            "s2","s1","low", 
                            "high","blocker", "critical", 
                            "major","minor", "trival", 
                            "enhancement","normal","s3",
                            "s4","null","",null
                        };
            string[] severityMeasure =    {
                            "high","high","low",
                            "high","high", "high",
                            "high","normal", "low", 
                            "low","normal","normal",
                            "low","normal","normal","normal"
                            };
            int index = severityList.IndexOf(item.ToLower());
            return severityMeasure[index];
        }

        private List<Developer> SimpleSearch(ProfileMatchConfig config, string[] queries) {
            List<Developer> searchResult = new List<Developer>();
            int sed = config.no_of_sed;
            int ned = config.no_of_ned;
            int fd = config.no_of_fd;
            foreach (var query in queries)
            {
                if (sed <= 0 && ned <= 0 && fd <= 0)
                {
                    break;
                }
                if (queries[0] == "General") {
                    int a = 0;
                }
                SearchResultCollection Results = searchManager.Search(query, 0, 1000, Searchable.AnalyzedFields.Values.ToArray());
              
                foreach (var result in Results.Data)
                {
                    result.Parse(x =>
                    {
                        result.Name = x.Get(Searchable.FieldStrings[Searchable.Field.Name]).Trim();
                        result.Product = x.Get(Searchable.FieldStrings[Searchable.Field.Product]);
                        result.Component = x.Get(Searchable.FieldStrings[Searchable.Field.Component]);
                        result.Skill = x.Get(Searchable.FieldStrings[Searchable.Field.Skill]);
                        result.DeveloperType = int.Parse(x.Get(Searchable.FieldStrings[Searchable.Field.DeveloperType]));
                    });
                    if (ned > 0 && result.DeveloperType == 2) {
                        searchResult.Add(new Developer { DeveloperName = result.Name, DeveloperId = result.DeveloperType });
                        ned--;
                        continue;
                    }
                    if (fd > 0 && result.DeveloperType == 3) {
                        searchResult.Add(new Developer { DeveloperName = result.Name, DeveloperId = result.DeveloperType });
                        fd--;
                        continue;
                    }
                    if (sed > 0 && result.DeveloperType == 1) {
                        searchResult.Add(new Developer { DeveloperName = result.Name, DeveloperId = result.DeveloperType });
                        sed--;
                        continue;
                    }
                   
                   
                  

                }
            }

             var finalResultset = searchResult.GroupBy(x => x.DeveloperName).Select(s => new Developer
                {
                    DeveloperName = s.Key,
                    DeveloperId = s.Select(x => x.DeveloperId).FirstOrDefault(),
                    current_workload = s.Select(x => x.DeveloperName).Count()
                }).OrderByDescending(x => x.current_workload).ToList();
            return finalResultset;
        }
        [HttpPost]
        public object PredictDeveloper()
        {
                    string searchType = Request.Form["type"].ToString();
                    IFormFile file = Request.Form.Files[0];

                    int type = int.Parse(searchType);
                    List<BugModel> bugs=GetBugsFromExcel(file);

                    string allProducts="";
                    string allComponents="";
                    string allDeveopers="";

                    decimal totalPrecision = 0;
                    decimal totalRecall = 0;
                    decimal totalFscore = 0;
                    foreach (var item in bugs)
                    {
                        allProducts += item.ProductName+",";
                        allComponents += item.ComponentName+",";
                        allDeveopers += item.AssigneeName+",";
                        string saverity = GetSaverityForSearch(item.SeverityTitle);
                        ProfileMatchConfig config = developerRepository.GetConfig(saverity);
                        int sed = config.no_of_sed;
                        int ned = config.no_of_ned;
                        int fd = config.no_of_fd;
                        string query = item.ComponentName;

                        string []queryOne = new[] { item.ComponentName, item.ProductName, item.KeyWords };
                        string []queryTwo = new[] { item.ComponentName+','+item.ProductName+','+item.KeyWords };

                        List<Developer> finalResultset = SimpleSearch(config, (type==1)? queryOne: queryTwo);

                        item.predictedDevelopers = finalResultset;


                        int match = 0;
                        finalResultset.ForEach(x => {
                           
                            var contains = item.AssigneeName.Contains(x.DeveloperName);
                            if (contains) match++;
                        });
                        decimal actualDevelopers = item.AssigneeName.Split(',').Count();
                        decimal methodDevelopers= finalResultset.Count;
                        methodDevelopers=(methodDevelopers == 0) ? methodDevelopers = 1: methodDevelopers;
                        actualDevelopers = (actualDevelopers == 0) ? actualDevelopers = 1: actualDevelopers;
                        decimal precision = (match / methodDevelopers);
                        decimal recall = match / actualDevelopers;
                        decimal precall = (precision + recall);
                        decimal fScore = 2 * ((precision * recall) / ((precall==0)?1:precall));

                        item.Precision = precision;
                        item.Recall = recall;
                        item.Fscore = fScore;

                        totalPrecision += precision;
                        totalRecall += recall;
                        totalFscore += fScore;


                    }

                    var summary = new SummaryModel();
                    summary.Bugs = bugs.Count();
                    summary.AvaragePrecesion = totalPrecision / summary.Bugs;
                    summary.AvarageRecall = totalRecall / summary.Bugs;
                    summary.AvarageFscore = totalFscore / summary.Bugs;

                    int productCount= allProducts.Split(',').Distinct().Count();
                    int ComponentCount = allComponents.Split(',').Distinct().Count();
                    summary.Products = productCount > 0 ? (productCount - 1) : 0;
                    summary.Components = ComponentCount > 0 ? (ComponentCount - 1) : 0;
                    summary.Developers = allDeveopers.Split(',').Distinct().Count();
                    return new {bugs,summary};
           
            //return this.Content(sb.ToString());
        }
        
        public IActionResult Search(string query) {
            SearchResultCollection Results= searchManager.Search(query, 0, 100, Searchable.AnalyzedFields.Values.ToArray());
            List<SearchResult> searchResult = new List<SearchResult>();
            foreach (var result in Results.Data)
            {
                result.Parse(x =>
                {
                    result.Name = x.Get(Searchable.FieldStrings[Searchable.Field.Name]);
                    result.Product = x.Get(Searchable.FieldStrings[Searchable.Field.Product]);
                    result.Component = x.Get(Searchable.FieldStrings[Searchable.Field.Component]);
                    result.Skill = x.Get(Searchable.FieldStrings[Searchable.Field.Skill]);
                });
                searchResult.Add(result);
            }
            return Json(searchResult);
        }
        #endregion
        public string ImportFromExcel()
        {
          
            IFormFile file = Request.Form.Files[0]; 
            var DeveloperType = Request.Form["DeveloperType"];
            List<DeveloperModel> developers = GetDevelopersFromExcel(file);

            foreach (var item in developers)
            {
                int developerType = int.Parse(DeveloperType.ToString() ?? "1");
                item.DeveloperTypeId = developerType;
            }

         

            int rows = developerRepository.ExecuteSql(developers);
            return rows + " Developers Uploaded";
      
        }
    }
}