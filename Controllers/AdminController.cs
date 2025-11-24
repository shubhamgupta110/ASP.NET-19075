using Microsoft.AspNetCore.Mvc;
using HotelManagement.Services;
using HotelManagement.Models;

namespace HotelManagement.Controllers
{
    public class AdminController : Controller
    {
        private readonly MongoService _mongoService;

        public AdminController(MongoService mongoService)
        {
            _mongoService = mongoService;
        }

        // ✅ Admin Login Page (GET)
        public IActionResult Login()
        {
            return View();
        }

        // ✅ Admin Login (POST)
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "admin123")
            {
                HttpContext.Session.SetString("AdminLoggedIn", "true");
                HttpContext.Session.SetString("UserName", "Admin");
                return RedirectToAction("Dashboard");
            }

            ViewBag.Error = "⚠️ Invalid admin credentials!";
            return View();
        }

        // ✅ Dashboard (only if admin logged in)
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("AdminLoggedIn") != "true")
                return RedirectToAction("Login");

            var users = _mongoService.GetAllUsers();
            return View(users);
        }

        // ✅ Manage Users
        public IActionResult ManageUsers()
        {
            if (HttpContext.Session.GetString("AdminLoggedIn") != "true")
                return RedirectToAction("Login");

            var users = _mongoService.GetAllUsers();
            return View(users);
        }

        // ✅ Manage Rooms
        public IActionResult ManageRooms()
        {
            if (HttpContext.Session.GetString("AdminLoggedIn") != "true")
                return RedirectToAction("Login");

            var rooms = _mongoService.GetRooms(); // Make sure MongoService has this method
            return View(rooms);
        }

        // ✅ Bookings
        public IActionResult Bookings()
        {
            if (HttpContext.Session.GetString("AdminLoggedIn") != "true")
                return RedirectToAction("Login");

            var bookings = _mongoService.GetAllBookings(); // Add this in MongoService
            return View(bookings);
        }

        // ✅ Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
