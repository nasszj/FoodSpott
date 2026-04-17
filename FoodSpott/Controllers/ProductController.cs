using Microsoft.AspNetCore.Mvc;
using ServiceLibrary.Models;
using ServiceLibrary.Services;

namespace FoodSpott.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            List<Product> products = _productService.GetAllProducts();
            return View(products);
        }

        public IActionResult Details(int id)
        {
            Product product = _productService.GetProductById(id);

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

            _productService.AddProduct(product);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Product product = _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        { 
            if (string.IsNullOrWhiteSpace(product.Name) || product.Price <= 0)
            {
                TempData["ErrorMessage"] = "Name and price are required.";
                    return View(product);
            }

            try
            {
                _productService.GetProductById(product.ProductID);
                TempData["ErrorMessage"] = "Product succesfully updated.";
                return RedirectToAction("Index");
            }

            catch
            {
                TempData["ErrorMessage"] = "Updating the product failed.";
                return View(product);
            }
        }
    }
}