using BabySphere.Data;
using BabySphere.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            string? role = HttpContext.Session.GetString("UserRole");

            if (role == "Admin")
            {
                return RedirectToAction("Dashboard", "Admin");
            }

            if (role == "Parent")
            {
                return RedirectToAction("ParentDashboard", "Home");
            }

            if (role == "Babysitter")
            {
                return RedirectToAction("BabysitterDashboard", "Home");
            }

            return View(
                "~/Views/Home/Login.cshtml",
                new LoginViewModel()
            );
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(
                    "~/Views/Home/Login.cshtml",
                    login
                );
            }

            var user = _context.UserAccounts.FirstOrDefault(
                u => u.Email == login.Email &&
                     u.Password == login.Password
            );

            if (user == null)
            {
                ModelState.AddModelError(
                    "",
                    "Invalid email or password."
                );

                return View(
                    "~/Views/Home/Login.cshtml",
                    login
                );
            }

            HttpContext.Session.SetInt32(
                "UserId",
                user.Id
            );

            HttpContext.Session.SetString(
                "UserName",
                user.FullName
            );

            HttpContext.Session.SetString(
                "UserRole",
                user.Role
            );

            if (user.Role == "Admin")
            {
                return RedirectToAction(
                    "Dashboard",
                    "Admin"
                );
            }

            if (user.Role == "Parent")
            {
                return RedirectToAction(
                    "ParentDashboard",
                    "Home"
                );
            }

            if (user.Role == "Babysitter")
            {
                return RedirectToAction(
                    "BabysitterDashboard",
                    "Home"
                );
            }

            HttpContext.Session.Clear();

            ModelState.AddModelError(
                "",
                "This account does not have a valid role."
            );

            return View(
                "~/Views/Home/Login.cshtml",
                login
            );
        }

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Login");
            }

            var babysittersList = _context.Babysitters.ToList();

            ViewBag.Babysitters = babysittersList;
            ViewBag.Authenticated = "true";
            ViewBag.UserName =
                HttpContext.Session.GetString("UserName");

            var dashboardStats = new AdminDashboardViewModel
            {
                TotalBabysitters = babysittersList.Count,

                TotalProducts = 4,

                TotalBookings = _context.Bookings.Count(),

                TotalParentProfiles =
                    _context.ParentProfiles.Count(),

                PendingSupportRequests =
                    _context.ParentProfiles.Count(
                        r => r.Status == "Pending"
                    )
            };

            dashboardStats.RecentActivities.Add(
                "Database connection verified successfully."
            );

            if (dashboardStats.TotalBabysitters > 0)
            {
                dashboardStats.RecentActivities.Add(
                    $"Loaded {dashboardStats.TotalBabysitters} " +
                    "babysitter records directly from SQL Server."
                );
            }

            return View(
                "~/Views/Home/Dashboard.cshtml",
                dashboardStats
            );
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
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Login");
            }

            if (ModelState.IsValid)
            {
                _context.Babysitters.Add(newSitter);
                _context.SaveChanges();
            }

            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public IActionResult DeleteBabysitter(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Login");
            }

            var sitter = _context.Babysitters.Find(id);

            if (sitter != null)
            {
                _context.Babysitters.Remove(sitter);
                _context.SaveChanges();
            }

            return RedirectToAction("Dashboard");
        }
    }
}