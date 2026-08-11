using BabySphere.Data;
using BabySphere.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text.Json;

namespace BabySphere.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }


        // =========================================================
        // HOME
        // =========================================================

        public IActionResult Index()
        {
            return View();
        }


        // =========================================================
        // BABYSITTERS
        // =========================================================

        [HttpGet]
        public IActionResult Babysitters(
            string searchTerm,
            int? minimumExperience,
            decimal? maximumRate,
            double? minimumRating)
        {
            var query = _context.Babysitters.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.Trim();

                query = query.Where(b =>
                    b.Name.Contains(searchTerm) ||
                    b.Skills.Contains(searchTerm)
                );
            }

            if (minimumExperience.HasValue)
            {
                query = query.Where(
                    b => b.Experience >= minimumExperience.Value
                );
            }

            if (maximumRate.HasValue)
            {
                query = query.Where(
                    b => b.HourlyRate <= maximumRate.Value
                );
            }

            if (minimumRating.HasValue)
            {
                query = query.Where(
                    b => b.Rating >= minimumRating.Value
                );
            }

            ViewBag.SearchTerm = searchTerm;
            ViewBag.MinimumExperience = minimumExperience;
            ViewBag.MaximumRate = maximumRate;
            ViewBag.MinimumRating = minimumRating;

            var sitters = query
                .OrderByDescending(b => b.Rating)
                .ThenBy(b => b.HourlyRate)
                .ToList();

            return View(sitters);
        }


        public IActionResult BabysitterDetails(int id)
        {
            var babysitter = _context.Babysitters
                .FirstOrDefault(x => x.Id == id);

            if (babysitter == null)
            {
                return NotFound();
            }

            return View(babysitter);
        }


        // =========================================================
        // BOOKING
        // =========================================================

        [HttpGet]
        public IActionResult Booking(string name)
        {
            var booking = new Booking
            {
                BabysitterName = name,
                BookingDate = DateTime.Today.AddDays(1),
                Status = "Pending"
            };

            return View(booking);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmBooking(Booking newBooking)
        {
            newBooking.Status = "Pending";

            if (newBooking.BookingDate.Date < DateTime.Today)
            {
                ModelState.AddModelError(
                    "BookingDate",
                    "Booking date cannot be in the past."
                );
            }

            if (newBooking.EndTime <= newBooking.StartTime)
            {
                ModelState.AddModelError(
                    "EndTime",
                    "End time must be later than start time."
                );
            }

            if (!ModelState.IsValid)
            {
                return View(
                    "Booking",
                    newBooking
                );
            }

            _context.Bookings.Add(newBooking);
            _context.SaveChanges();

            return View(
                "BookingConfirmation",
                newBooking
            );
        }


        // =========================================================
        // PARENT SUPPORT
        // =========================================================

        [HttpGet]
        public IActionResult ParentSupport()
        {
            return View(new ParentProfile());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ParentSupport(ParentProfile newProfile)
        {
            if (!ModelState.IsValid)
            {
                return View(newProfile);
            }

            newProfile.TicketNumber =
                "BS-" + Random.Shared.Next(1000, 9999);

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


        // =========================================================
        // PARENT DASHBOARD
        // =========================================================

        public IActionResult ParentDashboard()
        {
            var userRole =
                HttpContext.Session.GetString("UserRole");

            if (userRole != "Parent")
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


        // =========================================================
        // BABYSITTER DASHBOARD
        // =========================================================

        public IActionResult BabysitterDashboard()
        {
            var userRole =
                HttpContext.Session.GetString("UserRole");

            if (userRole != "Babysitter")
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
            var userRole =
                HttpContext.Session.GetString("UserRole");

            if (userRole != "Babysitter")
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
            var userRole =
                HttpContext.Session.GetString("UserRole");

            if (userRole != "Babysitter")
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


        // =========================================================
        // BABYSITTER BOOKINGS
        // =========================================================

        [HttpGet]
        public IActionResult BabysitterBookings()
        {
            if (
                HttpContext.Session.GetString("UserRole")
                != "Babysitter"
            )
            {
                return RedirectToAction(
                    "Login",
                    "Admin"
                );
            }

            string babysitterName =
                HttpContext.Session.GetString("UserName")
                ?? string.Empty;

            var bookings = _context.Bookings
                .Where(
                    b => b.BabysitterName == babysitterName
                )
                .OrderByDescending(
                    b => b.BookingDate
                )
                .ThenBy(
                    b => b.StartTime
                )
                .ToList();

            ViewBag.UserName = babysitterName;

            return View(bookings);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AcceptBooking(int id)
        {
            if (
                HttpContext.Session.GetString("UserRole")
                != "Babysitter"
            )
            {
                return RedirectToAction(
                    "Login",
                    "Admin"
                );
            }

            string babysitterName =
                HttpContext.Session.GetString("UserName")
                ?? string.Empty;

            var booking = _context.Bookings
                .FirstOrDefault(
                    b =>
                        b.Id == id &&
                        b.BabysitterName == babysitterName
                );

            if (
                booking != null &&
                booking.Status == "Pending"
            )
            {
                booking.Status = "Accepted";

                _context.SaveChanges();
            }

            return RedirectToAction(
                "BabysitterBookings"
            );
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RejectBooking(int id)
        {
            if (
                HttpContext.Session.GetString("UserRole")
                != "Babysitter"
            )
            {
                return RedirectToAction(
                    "Login",
                    "Admin"
                );
            }

            string babysitterName =
                HttpContext.Session.GetString("UserName")
                ?? string.Empty;

            var booking = _context.Bookings
                .FirstOrDefault(
                    b =>
                        b.Id == id &&
                        b.BabysitterName == babysitterName
                );

            if (
                booking != null &&
                booking.Status == "Pending"
            )
            {
                booking.Status = "Rejected";

                _context.SaveChanges();
            }

            return RedirectToAction(
                "BabysitterBookings"
            );
        }


        // =========================================================
        // PRODUCTS
        // =========================================================

        public IActionResult Products()
        {
            var products =
                _context.BabyProducts.ToList();

            return View(products);
        }

        public IActionResult ProductDetails(int id)
        {
            var product =
                _context.BabyProducts
                    .FirstOrDefault(
                        x => x.Id == id
                    );

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        private List<CartItem> GetCart()
        {
            var cartJson = HttpContext.Session.GetString("Cart");

            if (string.IsNullOrEmpty(cartJson))
            {
                return new List<CartItem>();
            }

            return JsonSerializer.Deserialize<List<CartItem>>(cartJson)
                   ?? new List<CartItem>();
        }


        private void SaveCart(List<CartItem> cart)
        {
            var cartJson = JsonSerializer.Serialize(cart);

            HttpContext.Session.SetString("Cart", cartJson);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int id)
        {
            var product = _context.BabyProducts
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            if (product.Quantity <= 0)
            {
                return RedirectToAction(
                    "ProductDetails",
                    new { id = id }
                );
            }

            var cart = GetCart();

            var existingItem =
                cart.FirstOrDefault(
                    x => x.ProductId == id
                );

            if (existingItem != null)
            {
                if (existingItem.Quantity < product.Quantity)
                {
                    existingItem.Quantity++;
                }
            }
            else
            {
                cart.Add(
                    new CartItem
                    {
                        ProductId = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        ImageUrl = product.ImageUrl,
                        Quantity = 1
                    }
                );
            }

            SaveCart(cart);

            TempData["CartMessage"] = "Added to cart successfully!";

            return RedirectToAction(
                "ProductDetails",
                new { id = id }
            );
        }


        public IActionResult Cart()
        {
            var cart = GetCart();

            return View(cart);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(int id)
        {
            var cart = GetCart();

            var item =
                cart.FirstOrDefault(
                    x => x.ProductId == id
                );

            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
            }

            return RedirectToAction("Cart");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult IncreaseCartQuantity(int id)
        {
            var cart = GetCart();

            var item = cart.FirstOrDefault(
                x => x.ProductId == id
            );

            if (item != null)
            {
                var product = _context.BabyProducts
                    .FirstOrDefault(p => p.Id == id);

                if (product != null &&
                    item.Quantity < product.Quantity)
                {
                    item.Quantity++;

                    SaveCart(cart);
                }
            }

            return RedirectToAction("Cart");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DecreaseCartQuantity(int id)
        {
            var cart = GetCart();

            var item = cart.FirstOrDefault(
                x => x.ProductId == id
            );

            if (item != null)
            {
                if (item.Quantity > 1)
                {
                    item.Quantity--;
                }
                else
                {
                    cart.Remove(item);
                }

                SaveCart(cart);
            }

            return RedirectToAction("Cart");
        }


        // =========================================================
        // SUPPORT HISTORY
        // =========================================================

        public IActionResult SupportHistory()
        {
            var history =
                _context.ParentProfiles.ToList();

            return View(history);
        }


        // =========================================================
        // CHILD WELLNESS
        // =========================================================

        public IActionResult ChildHealth()
        {
            return View();
        }


        // =========================================================
        // CONTACT
        // =========================================================

        public IActionResult Contact()
        {
            return View();
        }

        
    }
}