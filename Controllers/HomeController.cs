using BabySphere.Data;
using BabySphere.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
            var babysitter = _context.Babysitters.FirstOrDefault(x => x.Id == id);
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

                return View("BookingConfirmation", newBooking);
            }

            ViewBag.BabysitterName = newBooking.BabysitterName;
            return View("Booking", newBooking);
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
        [ValidateAntiForgeryToken]
        public IActionResult ParentSupport(ParentProfile newProfile)
        {
            if (ModelState.IsValid)
            {
                newProfile.TicketNumber = "BS-" + System.Random.Shared.Next(1000, 9999); 
                newProfile.Status = "Pending";


                if (newProfile.SupportCategory == "Babysitting Help")
                {
                    newProfile.Recommendation = "We recommend checking available babysitters based on location, experience, and availability.";
                }
                else if (newProfile.SupportCategory == "Feeding Support")
                {
                    newProfile.Recommendation = "We recommend creating a feeding routine and reviewing baby feeding tips.";
                }
                else if (newProfile.SupportCategory == "Sleep Routine")
                {
                    newProfile.Recommendation = "We recommend tracking nap times and creating a consistent sleep schedule.";
                }
                else if (newProfile.SupportCategory == "Product Guidance")
                {
                    newProfile.Recommendation = "We recommend checking baby products based on your child’s age and daily needs.";
                }
                else
                {
                    newProfile.Recommendation = "Our parent support team can review your request and guide you with the next step.";
                }

                _context.ParentProfiles.Add(newProfile);
                _context.SaveChanges();

                return View("ParentDashboard", newProfile);
            }

            return View(newProfile);
        }

        public IActionResult ParentDashboard()
        {
            return View();
        }

        public IActionResult SupportHistory()
        {
            var history = _context.ParentProfiles.ToList();
            return View(history);
        }

        public IActionResult AdminDashboard()
        {
            var dashboard = new AdminDashboardViewModel
            {
                TotalBabysitters = _context.Babysitters.Count(),
                TotalProducts = 4, 
                TotalBookings = _context.Bookings.Count(),
                TotalParentProfiles = _context.ParentProfiles.Count(),
                PendingSupportRequests = _context.ParentProfiles.Count(r => r.Status == "Pending")
            };

            if (dashboard.TotalParentProfiles > 0)
            {
                dashboard.RecentActivities.Add("New parent support profile submitted to the database system.");
                dashboard.RecentActivities.Add("Live support ticket trace successfully logged.");
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