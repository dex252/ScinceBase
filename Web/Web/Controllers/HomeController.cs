using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Web.Models.Db;
using Web.Models.Db.Properties;
using Web.Repositories;
using Web.ViewModels.Shared.Errors;
using ValueType = Web.Enums.ValueType;

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
            var item = new RootNode();

            item.Guid = Guid.NewGuid().ToString();
            item.Name = $"RootNode#{item.Guid}";
            item.Attribures = new List<Models.Db.Attribute>();

            var attr1 = new Models.Db.Attribute();
            attr1.ValueType = ValueType.BINARY;
            attr1.Name = "BINARY-1";
            attr1.Guid = Guid.NewGuid().ToString();
            attr1.BinarySettings = new BinaryProperty();
            attr1.BinarySettings.NormalValue = false;
            attr1.Periods = new List<Period>();
            var period1 = new Period()
            {
                PeriodNumber = 1,
                BinaryValue = true
            };
            var period2 = new Period()
            {
                PeriodNumber = 2,
                BinaryValue = false
            };
            attr1.Periods.Add(period1);
            attr1.Periods.Add(period2);

            var attr2 = new Models.Db.Attribute();
            attr2.ValueType = ValueType.BINARY;
            attr2.Name = "BINARY-2";
            attr2.Guid = Guid.NewGuid().ToString();
            attr2.BinarySettings = new BinaryProperty();
            attr2.BinarySettings.NormalValue = true;
            attr2.Periods = new List<Period>();
            var period3 = new Period()
            {
                PeriodNumber = 1,
                BinaryValue = true
            };
            var period4 = new Period()
            {
                PeriodNumber = 2,
                BinaryValue = false
            };
            var period5 = new Period()
            {
                PeriodNumber = 3,
                BinaryValue = true
            };
            attr2.Periods.Add(period3);
            attr2.Periods.Add(period4);
            attr2.Periods.Add(period5);

            var attr3 = new Models.Db.Attribute();
            attr3.ValueType = ValueType.INTEGER;
            attr3.Name = "INTEGER-1";
            attr3.Guid = Guid.NewGuid().ToString();
            attr3.IntegerSettings = new IntegerProperty();
            attr3.IntegerSettings.NormalValue = new List<int>() { 1, 2, 3 };
            attr3.Periods = new List<Period>();
            var period6 = new Period()
            {
                PeriodNumber = 1,
                NumberValue = 2
            };
            var period7 = new Period()
            {
                PeriodNumber = 2,
                NumberValue = 5
            };
            var period8 = new Period()
            {
                PeriodNumber = 3,
                NumberValue = 7
            };
            attr3.Periods.Add(period6);
            attr3.Periods.Add(period7);
            attr3.Periods.Add(period8);

            var attr4 = new Models.Db.Attribute();
            attr4.ValueType = ValueType.ENUMS;
            attr4.Name = "ENUMS-1";
            attr4.Guid = Guid.NewGuid().ToString();
            attr4.EnumSettings = new EnumProperty();
            attr4.EnumSettings.NormalValues = new List<string>() { "Один, Два, Три, Четыре" };
            attr4.Periods = new List<Period>();
            var period9 = new Period()
            {
                PeriodNumber = 1,
                EnumValues = new List<string>() { "Два", "Три" }
            };
            var period10 = new Period()
            {
                PeriodNumber = 2,
                EnumValues = new List<string>() { "Один" }
            };
            var period11 = new Period()
            {
                PeriodNumber = 3,
                EnumValues = new List<string>() { "Пять" }
            };
            attr4.Periods.Add(period9);
            attr4.Periods.Add(period10);
            attr4.Periods.Add(period11);

            var attr5 = new Models.Db.Attribute();
            attr5.ValueType = ValueType.INTERVAL;
            attr5.Name = "INTERVAL-1";
            attr5.Guid = Guid.NewGuid().ToString();
            attr5.IntervalSettings = new IntervalProperty();
            attr5.IntervalSettings.NormalIntervals = new List<Interval>();
            var int1 = new Interval() { Min = 1, Max = 6 };
            var int2 = new Interval() { Min = 9, Max = 12 };
            attr5.IntervalSettings.NormalIntervals.Add(int1);
            attr5.IntervalSettings.NormalIntervals.Add(int2);

            attr5.Periods = new List<Period>();
            var period12 = new Period()
            {
                PeriodNumber = 1,
                IntervalValues = new List<Interval>() { new Interval() { Min = 2, Max = 3} }
            };
            var period13 = new Period()
            {
                PeriodNumber = 2,
                IntervalValues = new List<Interval>() { new Interval() { Min = 4, Max = 5 } }
            };
            var period14 = new Period()
            {
                PeriodNumber = 3,
                IntervalValues = new List<Interval>() { new Interval() { Min = 7, Max = 11 } }
            };
            attr5.Periods.Add(period12);
            attr5.Periods.Add(period13);
            attr5.Periods.Add(period14);

            item.Attribures.Add(attr1);
            item.Attribures.Add(attr2);
            item.Attribures.Add(attr3);
            item.Attribures.Add(attr4);
            item.Attribures.Add(attr5);

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
