using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Db;
using Web.Repositories;
using Web.ViewModels.Redactor;

namespace Web.Controllers
{
    public class RedactorController : Controller
    {
        private ISmartRepository SmartRepository { get; set; }
        public RedactorController(ISmartRepository smartRepository)
        {
            SmartRepository = smartRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Redactor/Index.cshtml");
        }

        [HttpGet]
        public IActionResult GetAllEnums()
        {
            var enums = SmartRepository.GetAllEnums();

            var enumsViewModel = new EnumsViewModel();
            enumsViewModel.Values = enums;

            return Ok(enumsViewModel);
        }

        [HttpPost]
        public IActionResult InsertEnums([FromBody] EnumsValue data)
        {
            data.Guid = Guid.NewGuid().ToString();
            var result = SmartRepository.InsertNewEnums(data);

            return Ok(result);
        }
        
    }
}
