using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using ServiceLibrary.Services;
using FoodSpott.ViewModels;

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
    }
}