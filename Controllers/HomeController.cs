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

        private List<Babysitter> GetBabysitters()
        {
            return new List<Babysitter>
            {
                new Babysitter
                {
                    Id = 1,
                    Name = "Emily Carter",
                    Experience = 3,
                    HourlyRate = 15,
                    Rating = 4.5,
                    Skills = "Infant care, feeding, tutoring"
                },
                new Babysitter
                {
                    Id = 2,
                    Name = "Olivia Brown",
                    Experience = 5,
                    HourlyRate = 18,
                    Rating = 4.8,
                    Skills = "Child supervision, meal preparation"
                },
                new Babysitter
                {
                    Id = 3,
                    Name = "Sophia Wilson",
                    Experience = 4,
                    HourlyRate = 17,
                    Rating = 4.7,
                    Skills = "Homework help, bedtime routines"
                },
                new Babysitter
                {
                    Id = 4,
                    Name = "Emma Johnson",
                    Experience = 6,
                    HourlyRate = 20,
                    Rating = 4.9,
                    Skills = "Newborn care, feeding schedules"
                },
                new Babysitter
                {
                    Id = 5,
                    Name = "Ava Thompson",
                    Experience = 2,
                    HourlyRate = 14,
                    Rating = 4.4,
                    Skills = "Play activities, child engagement"
                },
                new Babysitter
                {
                    Id = 6,
                    Name = "Mia Davis",
                    Experience = 7,
                    HourlyRate = 22,
                    Rating = 5.0,
                    Skills = "Special needs care, tutoring"
                },
                new Babysitter
                {
                    Id = 7,
                    Name = "Charlotte Miller",
                    Experience = 5,
                    HourlyRate = 19,
                    Rating = 4.8,
                    Skills = "Meal preparation, homework support"
                },
                new Babysitter
                {
                    Id = 8,
                    Name = "Grace Anderson",
                    Experience = 4,
                    HourlyRate = 16,
                    Rating = 4.6,
                    Skills = "Bedtime routines, infant care"
                }
            };
        }


        public IActionResult Babysitters()
        {
            return View(GetBabysitters());
        }

        public IActionResult BabysitterDetails(int id)
        {
            var babysitter = GetBabysitters()
                                .FirstOrDefault(x => x.Id == id);

            return View(babysitter);
        }

        public IActionResult Booking(string name)
        {
            ViewBag.BabysitterName = name;
            return View();
        }

        [HttpPost]
        public IActionResult ConfirmBooking(string parentName,
                                            string babysitterName,
                                            DateTime date)
        {
            ViewBag.ParentName = parentName;
            ViewBag.BabysitterName = babysitterName;
            ViewBag.Date = date;

            return View("BookingConfirmation");
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