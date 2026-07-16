using BabySphere.Data;
using BabySphere.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; 
using System.Linq;

namespace BabySphere.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("IsAdmin") == "true")
            {
                return RedirectToAction("Dashboard");
            }
            return View("~/Views/Home/Login.cshtml", new LoginViewModel());
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var adminUser = _context.Admins
                    .FirstOrDefault(a => a.Email == login.Email && a.Password == login.Password);

                if (adminUser != null)
                {
                    HttpContext.Session.SetString("IsAdmin", "true");
                    return RedirectToAction("Dashboard");
                }

                ModelState.AddModelError("", "Invalid Admin Credentials!");
            }
            return View("~/Views/Home/Login.cshtml", login);
        }

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToAction("Login");
            }

            var babysittersList = _context.Babysitters.ToList();
            ViewBag.Babysitters = babysittersList;
            ViewBag.Authenticated = "true";

            var dashboardStats = new AdminDashboardViewModel
            {
                TotalBabysitters = babysittersList.Count,
                TotalProducts = 4, 
                TotalBookings = _context.Bookings.Count(), 
                TotalParentProfiles = _context.ParentProfiles.Count(), 
                PendingSupportRequests = _context.ParentProfiles.Count(r => r.Status == "Pending") 
            };

            dashboardStats.RecentActivities.Add("Database connection verified successfully.");
            if (dashboardStats.TotalBabysitters > 0)
            {
                dashboardStats.RecentActivities.Add($"Loaded {dashboardStats.TotalBabysitters} babysitter records directly from SQL Server.");
            }

            return View("~/Views/Home/Dashboard.cshtml", dashboardStats);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); 
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult AddBabysitter(Babysitter newSitter)
        {
            if (HttpContext.Session.GetString("IsAdmin") == "true")
            {
                _context.Babysitters.Add(newSitter);
                _context.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public IActionResult DeleteBabysitter(int id)
        {
            if (HttpContext.Session.GetString("IsAdmin") == "true")
            {
                var sitter = _context.Babysitters.Find(id);
                if (sitter != null)
                {
                    _context.Babysitters.Remove(sitter);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Dashboard");
        }
    }
}