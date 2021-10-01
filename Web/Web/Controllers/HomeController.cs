using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
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

        public IActionResult Index()
        {
            var reviewModel = SmartRepository.GetReview();
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
