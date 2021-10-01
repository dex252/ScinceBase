using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Web.ViewModels.Shared.Errors;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private ILogger<HomeController> Log { get; }

        public HomeController(ILogger<HomeController> logger)
        public HomeController(ILogger<HomeController> log, ISmartRepository smartRepository)
        {
            Log = log;
            SmartRepository = smartRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
