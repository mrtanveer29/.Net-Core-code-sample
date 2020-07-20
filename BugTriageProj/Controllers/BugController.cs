using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataAccess.Models;
using DataAccess.Models.IRepository;
using DataAccess.Models.StronglyTypes;
using BugTriage.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BugTriage.Search;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.Text.RegularExpressions;
using System.Reflection;

namespace BugTriage.Controllers
{
    public class BugController : Controller
    {
        private readonly IBugRepository bugRepository;
        private readonly IHostingEnvironment environment;
  

        public BugController(IBugRepository bugRepository, IHostingEnvironment environment)
        {
            this.bugRepository = bugRepository;
            this.environment = environment;
          


        }
        [Route("/")]
        public IActionResult ResolvedBugs()
        {
            return View();
        }
        public IActionResult NewBugs()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            IEnumerable<BugModel> bugs = (IEnumerable<BugModel>)bugRepository.GetBugs(0,0,string.Concat(id));
            BugModel fetched = bugs.FirstOrDefault(x => x.BugId == id);
            BugViewModel bugViewModel = new BugViewModel
            {
                BugId=fetched.BugId,
                ManualId=fetched.id,
                Assignee=fetched.AssigneeName,
              
                Products = fetched.ProductName,
                Component = fetched.ComponentName,
                Status = fetched.Status,
                Summery = fetched.Summary,
                Saverity = fetched.Severity,
                Priority = fetched.Priority,
                Description = fetched.Description,
                Hardware = fetched.HardwareName,
                KeyWords = fetched.KeyWords,
                version = fetched.version,
                Changed=fetched.Changed
            };
            return View(bugViewModel);
        }


   
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(BugViewModel bug)
        {
            if (ModelState.IsValid)
            {
                string summaryAndDesc = bug.Summery + " " + bug.Description;
                string colValue = summaryAndDesc.Replace("\'", String.Empty).Replace("{", string.Empty).Replace("}", string.Empty).Replace("\"", String.Empty);

                var keywords = TextFormaterTool.DataPreprocess(colValue);

                BugModel bugprofile = new BugModel
                {
                    id = bug.Id,
                    AssigneeName=bug.Assignee,
                    ProductName = bug.Products,
                    ComponentName = bug.Component,
                    HardwareName = bug.Hardware,
                    Severity = bug.Saverity,
                    Priority = bug.Priority,
                    Summary =bug.Summery,
                    KeyWords=keywords,
                    Status="New",
                    Description = bug.Description,
                    Changed="",
                    Source= "Bug Triage"
                };
                bool returnVal = (bool)bugRepository.SaveBugProfile(bugprofile);
                if (returnVal)
                {
                    return RedirectToAction("ResolvedBugs");
                }
                else
                {
                    ModelState.AddModelError("db", "Cannot be saved");
                }

            }


            return View(bug);
        }
        [HttpGet]
        public IActionResult SearchProfileConfig() {
            return View();
        }
        [HttpPost]
        public IActionResult SearchProfileConfig(ProfileMatchConfigViewModel config)
        {
            if (ModelState.IsValid) {
                ProfileMatchConfig matchConfig = new ProfileMatchConfig
                {
                    profile_match_config_id = config.profile_match_config_id,
                    severity = config.severity,
                    no_of_sed = config.no_of_sed,
                    no_of_ned = config.no_of_ned,
                    no_of_fd = config.no_of_fd,
                };
                Confirmation res=bugRepository.SaveSearchProfileConfig(matchConfig);
                if (res.is_succeed)
                {
                    ViewData["success"] = res.msg;
                }
                else
                {
                    ViewData["error"] = res.msg;
                }


            }
            return View(config);
        }

        #region API
     
        [HttpPost]
        public IActionResult GetBugs()
        {
           
          
            var start = Request.Form["start"];
            var length = Request.Form["length"];
            string search = Request.Form["search[value]"];
            string orderbyId = Request.Form["columns[0][orderable]"];
            string orderbydir = Request.Form["order[0][dir]"];
            string status = Request.Form["status"];
            int startNo = int.Parse(start);
            int lengthNo = int.Parse(length);
            int bugCount = bugRepository.GetBugCount(); 
          
            IEnumerable<BugModel> bugs = (IEnumerable<BugModel>)bugRepository.GetBugs(startNo,lengthNo,search,status);
            if (bool.Parse(orderbyId)) {

                if(orderbydir=="asc")
                    bugs=bugs.OrderBy(x => x.id);
                else
                    bugs = bugs.OrderByDescending(x => x.id);
            }

            var ext = new { bugs,iTotalRecords = bugCount, iTotalDisplayRecords = bugCount};
            return Json(ext) ;



        }

        [HttpPost]
        public IActionResult Delete(string source)
        {
            var result = bugRepository.Delete(source);
            return Json(result);
        }
        [HttpGet]
        public IActionResult GetSearchProfileConfig(string severity)
        {
            var profileMatchConfig = bugRepository.GetSearchProfileConfig(severity);
            return Json(profileMatchConfig);
        }
        #endregion

     
            public string FileUpload()
        {

            IFormFile file = Request.Form.Files[0];
            string formData = Request.Form["bugSource"];
            string bugSource = formData?.ToString() ?? "";

            string ActualFileName = "";
            bool checkFileSave = false;
           // Hashtable hsTable = new Hashtable();
            if (file == null)
            {

                return "File not Upload to Server!";
            }
            else
            {
                /** save the File to Server Path **/
                ActualFileName = file.FileName;
                string[] explodFileName = ActualFileName.Split('.');
                ActualFileName = explodFileName[0] + "_" + bugSource+"."+ explodFileName.Last();
                if (explodFileName.Last() == "xlsx")
                {
                   
                    try
                    {
                        string folderName = "BugExcelDump";
                        // Save the uploaded file to "UploadedFiles" folder
                        string webRootPath = environment.WebRootPath;
                        string newPath = Path.Combine(webRootPath, folderName);
                        if (!Directory.Exists(newPath))
                            Directory.CreateDirectory(newPath);
                        /** end Save file to Server path */
                        string fullPath = Path.Combine(newPath, ActualFileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                            
                        checkFileSave = true;
                    }
                    catch
                    {
                        checkFileSave = false;
                    }

                    if (checkFileSave == true)
                    {
                        return "File Upload Succesfully";
                    }
                    else
                    {
                        return "Something went wrong when upload the file to database!";
                    }

                }
                else
                {
                    return "Please dump only Execl Workbook (xlsx) format Execl File.";
                }

            }

        }
      
    }
}