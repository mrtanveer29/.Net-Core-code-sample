using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BugTriage.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }
        [Route("/Error/{errorCode}")]
        public IActionResult Index(int errorCode)
        {
            IStatusCodeReExecuteFeature feature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
         
            switch (errorCode) {
                case 404: {
                    ViewBag.Message = "Page Could Not be Found";
                    break;
                }
            }
            return View();
        }
        [Route("/Error")]
        public IActionResult GlobalExceptionHandler()
        {
            IExceptionHandlerPathFeature feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            logger.LogError($"The path {feature.Path} " + $"threw an exception {feature.Error}");
            switch (404)
            {
                case 404:
                    {
                        ViewBag.Message = "Page Could Not be Found";
                        break;
                    }
            }
            return View();
        }
    }
}