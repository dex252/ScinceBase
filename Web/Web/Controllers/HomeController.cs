using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Web.Models.Db;
using Web.Models.Db.Properties;
using Web.Repositories;
using Web.ViewModels.Shared.Errors;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private ILogger<HomeController> Log { get; }

        private ISmartRepository SmartRepository { get; }

        public HomeController(ILogger<HomeController> log, ISmartRepository smartRepository)
        {
            Log = log;
            SmartRepository = smartRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var reviewModel = SmartRepository.GetReview();
            return View();
        }

        [HttpGet]
        public IActionResult Insert()
        {
            var item = new Classes();

            item.Name = "Второй пионер";
            item.Guid = Guid.NewGuid().ToString();
            item.Properties = new List<IProperty>();

            item.Properties.Add(new BinaryProperty() { Name = "123" });
            item.Properties.Add(new BinaryProperty() { Name = "456" });

            var id = SmartRepository.InsertClass(item);

            item.Id = id;

            return Ok(id);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = SmartRepository.GetClasses();

            return Ok(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
