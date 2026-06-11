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


        private List<BabyProduct> GetBabyProducts()
        {
            return new List<BabyProduct>
    {
        new BabyProduct
        {
            Id = 1,
            Name = "Baby Stroller",
            Category = "Travel",
            Price = 149.99m,
            Quantity = 15,
            Rating =4.7,
            Description = "Comfortable stroller for outdoor travel.",
            ImageUrl = "/images/stroller.jpg"
        },
        new BabyProduct
        {
            Id = 2,
            Name = "Baby Car Seat",
            Category = "Travel",
            Price = 199.99m,
            Quantity = 10,
            Rating =4.5,
            Description = "Safe and secure car seat for babies.",
            ImageUrl = "/images/carseat.jpg"
        },
        new BabyProduct
        {
            Id = 3,
            Name = "Diaper Bag",
            Category = "Travel",
            Price = 39.99m,
            Quantity = 19,
            Rating =4.9,
            Description = "Spacious diaper bag for parents.",
            ImageUrl = "/images/diaperbag.jpg"
        },

        new BabyProduct
        {
            Id = 4,
            Name = "Baby Bottle",
            Category = "Feeding",
            Price = 12.99m,
            Quantity = 15,
            Rating =4.3,
            Description = "BPA-free feeding bottle.",
            ImageUrl = "/images/bottle.jpg"
        },
        new BabyProduct
        {
            Id = 5,
            Name = "High Chair",
            Category = "Feeding",
            Price = 89.99m,
            Quantity = 8,
            Rating =4.3,
            Description = "Comfortable feeding chair for babies.",
            ImageUrl = "/images/highchair.jpg"
        },
        new BabyProduct
        {
            Id = 6,
            Name = "Baby Bibs",
            Category = "Feeding",
            Price = 9.99m,
            Quantity = 23,
            Rating =3.8,
            Description = "Soft bibs to keep clothes clean.",
            ImageUrl = "/images/bibs.jpg"
        },

        new BabyProduct
        {
            Id = 7,
            Name = "Baby Lotion",
            Category = "Care",
            Price = 8.99m,
            Quantity = 12,
            Rating =4.8,
            Description = "Gentle lotion for baby skin.",
            ImageUrl = "/images/lotion.jpg"
        },
        new BabyProduct
        {
            Id = 8,
            Name = "Baby Shampoo",
            Category = "Care",
            Price = 7.99m,
            Quantity = 17,
            Rating =4.8,
            Description = "Tear-free shampoo for babies.",
            ImageUrl = "/images/shampoo.jpg"
        },
        new BabyProduct
        {
            Id = 9,
            Name = "Diapers Pack",
            Category = "Care",
            Price = 24.99m,
            Quantity = 20,
            Rating =4.6,
            Description = "Soft and comfortable diapers.",
            ImageUrl = "/images/diapers.jpg"
        },

        new BabyProduct
        {
            Id = 10,
            Name = "Building Blocks",
            Category = "Toys",
            Price = 19.99m,
            Quantity = 23,
            Rating =3.7,
            Description = "Educational blocks for learning.",
            ImageUrl = "/images/blocks.jpg"
        },
        new BabyProduct
        {
            Id = 11,
            Name = "Teddy Bear",
            Category = "Toys",
            Price = 14.99m,
            Quantity = 27,
            Rating =3.6,
            Description = "Soft plush teddy bear.",
            ImageUrl = "/images/teddy.jpg"
        },
        new BabyProduct
        {
            Id = 12,
            Name = "Baby Rattle",
            Category = "Toys",
            Price = 6.99m,
            Quantity = 16,
            Rating =4.0,
            Description = "Colorful rattle toy for babies.",
            ImageUrl = "/images/rattle.jpg"
        }
    };
        }


        public IActionResult Products()
        {
            return View(GetBabyProducts());
        }

        public IActionResult ProductDetails(int id)
        {
            var product = GetBabyProducts().FirstOrDefault(x => x.Id == id);

            return View(product);
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