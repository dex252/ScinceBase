using Microsoft.AspNetCore.Mvc;
using Web.Managers;
using Web.Models.Settings;
using Web.Repositories;

namespace Web.Controllers
{
    public class GeneratorController : Controller
    {
        private IGeneratorManager GeneratorManager { get; }
        private ISmartRepository SmartRepository { get; }

        public GeneratorController(IGeneratorManager generatorManager, ISmartRepository smartRepository)
        {
            GeneratorManager = generatorManager;
            SmartRepository = smartRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Generator/Generator.cshtml");
        }

        [HttpPost]
        public IActionResult SetGeneratorParametres([FromBody] SettingsValues data)
        {
            var nodes = GeneratorManager.GenerateNodes(data);
            SmartRepository.SaveClasses(nodes);
            return Ok(nodes);
        }
    }
}
