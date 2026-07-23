using BabySphere.Data;
using BabySphere.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BabySphere.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Babysitters()
        {
            var sitters = _context.Babysitters.ToList();

            return View(sitters);
        }

        public IActionResult BabysitterDetails(int id)
        {
            var babysitter =
                _context.Babysitters.FirstOrDefault(x => x.Id == id);

            if (babysitter == null)
            {
                return NotFound();
            }

            return View(babysitter);
        }

        public IActionResult Booking(string name)
        {
            ViewBag.BabysitterName = name;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmBooking(Booking newBooking)
        {
            if (ModelState.IsValid)
            {
                _context.Bookings.Add(newBooking);
                _context.SaveChanges();

                return View(
                    "BookingConfirmation",
                    newBooking
                );
            }

            ViewBag.BabysitterName =
                newBooking.BabysitterName;

            return View(
                "Booking",
                newBooking
            );
        }

        [HttpGet]
        public IActionResult ParentSupport()
        {
            return View(new ParentProfile());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ParentSupport(ParentProfile newProfile)
        {
            if (ModelState.IsValid)
            {
                newProfile.TicketNumber =
                    "BS-" + System.Random.Shared.Next(1000, 9999);

                newProfile.Status = "Pending";

                if (newProfile.SupportCategory == "Babysitting Help")
                {
                    newProfile.Recommendation =
                        "We recommend checking available babysitters based on location, experience, and availability.";
                }
                else if (newProfile.SupportCategory == "Feeding Support")
                {
                    newProfile.Recommendation =
                        "We recommend creating a feeding routine and reviewing baby feeding tips.";
                }
                else if (newProfile.SupportCategory == "Sleep Routine")
                {
                    newProfile.Recommendation =
                        "We recommend tracking nap times and creating a consistent sleep schedule.";
                }
                else if (newProfile.SupportCategory == "Product Guidance")
                {
                    newProfile.Recommendation =
                        "We recommend checking baby products based on your child’s age and daily needs.";
                }
                else
                {
                    newProfile.Recommendation =
                        "Our parent support team can review your request and guide you with the next step.";
                }

                _context.ParentProfiles.Add(newProfile);
                _context.SaveChanges();

                ViewBag.UserName =
                    HttpContext.Session.GetString("UserName");

                return View(
                    "ParentDashboard",
                    newProfile
                );
            }

            return View(newProfile);
        }

        public IActionResult ParentDashboard()
        {
            if (HttpContext.Session.GetString("UserRole") != "Parent")
            {
                return RedirectToAction(
                    "Login",
                    "Admin"
                );
            }

            ViewBag.UserName =
                HttpContext.Session.GetString("UserName");

            return View(new ParentProfile());
        }

        public IActionResult BabysitterDashboard()
        {
            if (HttpContext.Session.GetString("UserRole") != "Babysitter")
            {
                return RedirectToAction(
                    "Login",
                    "Admin"
                );
            }

            ViewBag.UserName =
                HttpContext.Session.GetString("UserName");

            return View();
        }

        public IActionResult BabysitterProfile()
        {
            if (HttpContext.Session.GetString("UserRole") != "Babysitter")
            {
                return RedirectToAction(
                    "Login",
                    "Admin"
                );
            }

            ViewBag.UserName =
                HttpContext.Session.GetString("UserName");

            return View();
        }

        public IActionResult BabysitterAvailability()
        {
            if (HttpContext.Session.GetString("UserRole") != "Babysitter")
            {
                return RedirectToAction(
                    "Login",
                    "Admin"
                );
            }

            ViewBag.UserName =
                HttpContext.Session.GetString("UserName");

            return View();
        }

        public IActionResult BabysitterBookings()
        {
            if (HttpContext.Session.GetString("UserRole") != "Babysitter")
            {
                return RedirectToAction(
                    "Login",
                    "Admin"
                );
            }

            ViewBag.UserName =
                HttpContext.Session.GetString("UserName");

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
                    Rating = 4.7,
                    Description =
                        "Comfortable stroller for outdoor travel.",
                    ImageUrl = "/images/stroller.jpg"
                },

                new BabyProduct
                {
                    Id = 2,
                    Name = "Baby Car Seat",
                    Category = "Travel",
                    Price = 199.99m,
                    Quantity = 10,
                    Rating = 4.5,
                    Description =
                        "Safe and secure car seat for babies.",
                    ImageUrl = "/images/carseat.jpg"
                },

                new BabyProduct
                {
                    Id = 3,
                    Name = "Diaper Bag",
                    Category = "Travel",
                    Price = 39.99m,
                    Quantity = 19,
                    Rating = 4.9,
                    Description =
                        "Spacious diaper bag for parents.",
                    ImageUrl = "/images/diaperbag.jpg"
                },

                new BabyProduct
                {
                    Id = 4,
                    Name = "Baby Bottle",
                    Category = "Feeding",
                    Price = 12.99m,
                    Quantity = 15,
                    Rating = 4.3,
                    Description =
                        "BPA-free feeding bottle.",
                    ImageUrl = "/images/bottle.jpg"
                },

                new BabyProduct
                {
                    Id = 5,
                    Name = "High Chair",
                    Category = "Feeding",
                    Price = 89.99m,
                    Quantity = 8,
                    Rating = 4.3,
                    Description =
                        "Comfortable feeding chair for babies.",
                    ImageUrl = "/images/highchair.jpg"
                },

                new BabyProduct
                {
                    Id = 6,
                    Name = "Baby Bibs",
                    Category = "Feeding",
                    Price = 9.99m,
                    Quantity = 23,
                    Rating = 3.8,
                    Description =
                        "Soft bibs to keep clothes clean.",
                    ImageUrl = "/images/bibs.jpg"
                },

                new BabyProduct
                {
                    Id = 7,
                    Name = "Baby Lotion",
                    Category = "Care",
                    Price = 8.99m,
                    Quantity = 12,
                    Rating = 4.8,
                    Description =
                        "Gentle lotion for baby skin.",
                    ImageUrl = "/images/lotion.jpg"
                },

                new BabyProduct
                {
                    Id = 8,
                    Name = "Baby Shampoo",
                    Category = "Care",
                    Price = 7.99m,
                    Quantity = 17,
                    Rating = 4.8,
                    Description =
                        "Tear-free shampoo for babies.",
                    ImageUrl = "/images/shampoo.jpg"
                },

                new BabyProduct
                {
                    Id = 9,
                    Name = "Diapers Pack",
                    Category = "Care",
                    Price = 24.99m,
                    Quantity = 20,
                    Rating = 4.6,
                    Description =
                        "Soft and comfortable diapers.",
                    ImageUrl = "/images/diapers.jpg"
                },

                new BabyProduct
                {
                    Id = 10,
                    Name = "Building Blocks",
                    Category = "Toys",
                    Price = 19.99m,
                    Quantity = 23,
                    Rating = 3.7,
                    Description =
                        "Educational blocks for learning.",
                    ImageUrl = "/images/blocks.jpg"
                },

                new BabyProduct
                {
                    Id = 11,
                    Name = "Teddy Bear",
                    Category = "Toys",
                    Price = 14.99m,
                    Quantity = 27,
                    Rating = 3.6,
                    Description =
                        "Soft plush teddy bear.",
                    ImageUrl = "/images/teddy.jpg"
                },

                new BabyProduct
                {
                    Id = 12,
                    Name = "Baby Rattle",
                    Category = "Toys",
                    Price = 6.99m,
                    Quantity = 16,
                    Rating = 4.0,
                    Description =
                        "Colorful rattle toy for babies.",
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
            var product =
                GetBabyProducts().FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult SupportHistory()
        {
            var history =
                _context.ParentProfiles.ToList();

            return View(history);
        }

        public IActionResult ChildHealth()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}