using Microsoft.AspNetCore.Mvc;

namespace BabySphere.Controllers
{
    public class ParentSupportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}