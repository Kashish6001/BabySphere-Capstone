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
                return View("ParentDashboard", profile);
            }

            return View(profile);
        }

        public IActionResult Contact()
        {
            return View();
        }

       
       

        
    }
}
