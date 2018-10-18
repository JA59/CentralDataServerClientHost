using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using iCDataCenterClientHost.CustomIdentity;
using Microsoft.Extensions.Configuration;
using iCDataCenterClientHost.ViewModels;
using System.Diagnostics;

namespace iCDataCenterClientHost.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
