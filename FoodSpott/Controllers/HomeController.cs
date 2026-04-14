using ServiceLibrary.Models;
using FoodSpott.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FoodSpott.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductRepository _repo;

        public HomeController(ProductRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
