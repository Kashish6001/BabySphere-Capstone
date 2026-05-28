using System.Diagnostics;
using BabySphere.Models;
using Microsoft.AspNetCore.Mvc;

namespace BabySphere.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Babysitters()
        {
            return View();
        }

        public IActionResult Products()
        {
            return View();
        }

        public IActionResult ParentSupport()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
