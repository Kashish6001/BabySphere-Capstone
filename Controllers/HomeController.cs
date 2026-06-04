using BabySphere.Models;
using Microsoft.AspNetCore.Mvc;

namespace BabySphere.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private static List<ParentProfile> supportRequests = new List<ParentProfile>();

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

        public IActionResult BabysitterDetails()
        {
            return View();
        }

        public IActionResult Booking(string name)
        {
            ViewBag.BabysitterName = name;
            return View();
        }

        public IActionResult Products()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ParentSupport()
        {
            return View(new ParentProfile());
        }

        [HttpPost]
        public IActionResult ParentSupport(ParentProfile profile)
        {
            if (ModelState.IsValid)
            {
                profile.TicketNumber = "BS-" + Random.Shared.Next(1000, 9999);
                profile.Status = "Pending";

                if (profile.SupportCategory == "Babysitting Help")
                {
                    profile.Recommendation = "We recommend checking available babysitters based on location, experience, and availability.";
                }
                else if (profile.SupportCategory == "Feeding Support")
                {
                    profile.Recommendation = "We recommend creating a feeding routine and reviewing baby feeding tips.";
                }
                else if (profile.SupportCategory == "Sleep Routine")
                {
                    profile.Recommendation = "We recommend tracking nap times and creating a consistent sleep schedule.";
                }
                else if (profile.SupportCategory == "Product Guidance")
                {
                    profile.Recommendation = "We recommend checking baby products based on your child’s age and daily needs.";
                }
                else
                {
                    profile.Recommendation = "Our parent support team can review your request and guide you with the next step.";
                }

                supportRequests.Add(profile);

                return View("ParentDashboard", profile);
            }

            return View(profile);
        }

        public IActionResult ParentDashboard()
        {
            return View();
        }

        public IActionResult SupportHistory()
        {
            return View(supportRequests);
        }

        public IActionResult AdminDashboard()
        {
            var dashboard = new AdminDashboardViewModel
            {
                TotalBabysitters = 3,
                TotalProducts = 4,
                TotalBookings = 2,
                TotalParentProfiles = supportRequests.Count,
                PendingSupportRequests = supportRequests.Count(r => r.Status == "Pending")
            };

            

            if (supportRequests.Count > 0)
            {
                dashboard.RecentActivities.Add("New parent support profile submitted.");
                dashboard.RecentActivities.Add("Support ticket created and saved in temporary history.");
            }

            return View(dashboard);
        }

        public IActionResult ChildHealth()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Email = login.Email;
                return View("LoginSuccess");
            }

            return View(login);
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}