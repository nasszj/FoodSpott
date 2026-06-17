using DAL.Repositories;
using FoodSpott.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ServiceLibrary.Models;
using ServiceLibrary.Services;

namespace FoodSpott.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;

        public AccountController(IConfiguration configuration)
        {
            _userService = new UserService(new UserRepository(configuration));
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            bool success = _userService.Register(
                model.Email,
                model.Password,
                model.ConfirmPassword);

            if (success)
            {
                return RedirectToAction("Index", "Product");
            }

            TempData["ErrorMessage"] = "Registration failed.";

            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            try
            {
                User user = _userService.Login(model.Email, model.Password);

                if (user == null)
                {
                    TempData["ErrorMessage"] = "Invalid email or password.";
                    return View(model);
                }

                HttpContext.Session.SetInt32("UserID", user.UserID);
                HttpContext.Session.SetString("Email", user.Email);
                HttpContext.Session.SetString("Role", user.Role);

                return RedirectToAction("Index", "Product");
            }
            catch
            {
                TempData["ErrorMessage"] = "Something went wrong. Please try again later.";
                return View(model);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Product");
        }
    }
}