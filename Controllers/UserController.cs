using HotelManagement.Models;
using HotelManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace HotelManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly MongoService _mongoService;

        public UserController(MongoService mongoService)
        {
            _mongoService = mongoService;
        }

        // ✅ --- REGISTER (same Login.cshtml page use karega)
        [HttpGet]
        public IActionResult Register()
        {
            // Login.cshtml hi use hoga, toggle kar ke Register form show karenge
            return View("Login");
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _mongoService.AddUser(user);
                TempData["Success"] = "🎉 Registration successful! Please login now.";
                return RedirectToAction("Login");
            }

            TempData["Error"] = "⚠️ Something went wrong. Try again!";
            return View("Login");
        }

        // ✅ --- LOGIN ---
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _mongoService.ValidateUser(email, password);
            if (user == null)
            {
                ViewBag.Message = "Invalid email or password.";
                return View();
            }

            // ✅ Save username in session after login
            HttpContext.Session.SetString("UserName", user.Name);
            HttpContext.Session.SetString("UserRole", user.Role ?? "User");

            // ✅ Redirect based on Role
            if (user.Role == "Admin")
                return RedirectToAction("Dashboard", "Admin");
            else
                return RedirectToAction("Dashboard");
        }

        // ✅ --- USER DASHBOARD ---
        public IActionResult Dashboard()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }

        // ✅ --- PROFILE ---
        public IActionResult Profile()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }

        // ✅ --- MY BOOKINGS ---
        public IActionResult MyBookings()
        {
            string? name = HttpContext.Session.GetString("UserName");
            var bookings = _mongoService.GetUserBookings(name ?? "");
            return View(bookings);
        }

        // ✅ --- FEEDBACK ---
        public IActionResult Feedback() => View();

        // ✅ --- PAYMENTS ---
        public IActionResult Payments()
        {
            var payments = _mongoService.GetPayments();
            return View(payments);
        }

        // ✅ --- LOGOUT ---
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
