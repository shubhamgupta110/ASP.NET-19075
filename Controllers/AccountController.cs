using HotelManagement.Services;
using Microsoft.AspNetCore.Mvc;
using HotelManagement.Models;

namespace HotelManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly MongoService _mongoService;

        public AccountController(MongoService mongoService)
        {
            _mongoService = mongoService;
        }

        // ✅ Show login page
        public IActionResult Login()
        {
            return View();
        }

        // ✅ Handle login POST
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _mongoService.ValidateUser(email, password);

            if (user != null)
            {
                // ✅ Role-based redirection
                if (user.Role == "Admin")
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
                else
                {
                    return RedirectToAction("Dashboard", "User");
                }
            }

            ViewBag.Error = "Invalid email or password!";
            return View();
        }

        // ✅ Show registration page
        public IActionResult Register()
        {
            return View();
        }

        // ✅ Handle registration POST
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(user.Role))
                    user.Role = "User"; // default role

                _mongoService.AddUser(user);
                return RedirectToAction("Login");
            }

            return View(user);
        }
    }
}
