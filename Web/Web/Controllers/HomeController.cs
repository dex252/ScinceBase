using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Web.Models.Db;
using Web.Models.Db.Periods;
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
            return View("~/Views/Home/Index.cshtml", reviewModel);
        }

        [HttpGet]
        public IActionResult Insert()
        {
            var item = new Classes();

            item.Name = "Второй пионер";
            item.Guid = Guid.NewGuid().ToString();
            item.Properties = new List<KeyValuePair<Enums.ValueType, IProperty>>();

            item.Properties.Add(new KeyValuePair<Enums.ValueType, IProperty>(Enums.ValueType.BINARY, new BinaryProperty() { Name = "BINARY-1", ValueType = Enums.ValueType.BINARY, NormalValue =  false, Periods = new List<IPeriod>() { new BinaryPeriod() {PeriodValue = true } , new BinaryPeriod() { PeriodValue = false} }  }));
            item.Properties.Add(new KeyValuePair<Enums.ValueType, IProperty>(Enums.ValueType.BINARY, new BinaryProperty() { Name = "BINARY-2", ValueType = Enums.ValueType.BINARY, NormalValue = true, Periods = new List<IPeriod>() { new BinaryPeriod() { PeriodValue = true }, new BinaryPeriod() { PeriodValue = false }, new BinaryPeriod() { PeriodValue = true } } }));
            item.Properties.Add(new KeyValuePair<Enums.ValueType, IProperty>(Enums.ValueType.INTEGER, new IntegerProperty() { Name = "INTEGER-1", ValueType = Enums.ValueType.INTEGER, NormalValue = new List<int>() { 1,2,3}, CurrentValue = 2, Periods = new List<IPeriod>() { new IntegerPeriod() { PeriodValue = 2 }, new IntegerPeriod() { PeriodValue = 3 }, new IntegerPeriod() { PeriodValue = 4 } } }));
            item.Properties.Add(new KeyValuePair<Enums.ValueType, IProperty>(Enums.ValueType.ENUMS, new EnumProperty() { Name = "ENUMS-1", ValueType = Enums.ValueType.ENUMS, NormalValues = new List<string>() { "Один, Два, Три, Четыре"}, CurrentValues = new List<string>() { "Два", "Три"}, Periods = new List<IPeriod>() { new EnumPeriod() { PeriodValue = new List<string>() { "Один" } } } }));
            item.Properties.Add(new KeyValuePair<Enums.ValueType, IProperty>(Enums.ValueType.INTERVAL, new IntervalProperty() { Name = "INTERVAL-1", ValueType = Enums.ValueType.INTERVAL, NormalIntervals = new List<Interval>() { new Interval() { Min = 1, Max = 5}, new Interval() { Min = 7, Max = 12} }, Periods = new List<IPeriod>() { new IntervalPeriod() { PeriodValue = 3 }, new IntervalPeriod() { PeriodValue = 8 } } }));

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
