using FoodSpott.Models;
using FoodSpott.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FoodSpott.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;

        public ProductController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            List<Product> products = _productRepository.GetAllProducts();
            return View(products);
        }

        public IActionResult Details(int id)
        {
            Product product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            _productRepository.AddProduct(product);
            return RedirectToAction("Index");
        }
    }
}