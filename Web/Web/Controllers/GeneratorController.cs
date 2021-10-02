using Microsoft.AspNetCore.Mvc;
using Web.Managers;
using Web.Models.Settings;

namespace Web.Controllers
{
    public class GeneratorController : Controller
    {
        private IGeneratorManager GeneratorManager { get; }
        public GeneratorController(IGeneratorManager generatorManager)
        {
            GeneratorManager = generatorManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Generator/Generator.cshtml");
        }

        [HttpPost]
        public IActionResult SetGeneratorParametres([FromBody] SettingsValues data)
        {
            GeneratorManager.GenerateNodes(data);
            return Ok(data);
        }
    }
}
