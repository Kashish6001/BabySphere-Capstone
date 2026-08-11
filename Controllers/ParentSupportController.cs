using Microsoft.AspNetCore.Mvc;

namespace BabySphere.Controllers
{
    public class ParentSupportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InfantCare()
        {
            return View();
        }

        public IActionResult Nutrition()
        {
            return View();
        }

        public IActionResult Development()
        {
            return View();
        }

        public IActionResult Sleep()
        {
            return View();
        }
    }
}