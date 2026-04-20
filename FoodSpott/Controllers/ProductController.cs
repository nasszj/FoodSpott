using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ServiceLibrary.Models;
using ServiceLibrary.Services;

namespace FoodSpott.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(IConfiguration configuration)
        {
            _productService = new ProductService(new ProductRepository(configuration));
        }

        public IActionResult Index(string category)
        {
            List<Product> products = _productService.GetAllProducts(category);
            ViewBag.CurrentCategory = category;
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
                TempData["SuccessMessage"] = "Product succesfully updated.";
                return RedirectToAction("Index");
            }

            catch
            {
                TempData["ErrorMessage"] = "Updating the product failed.";
                return View(product);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        { 
            Product product = _productService.GetProductById(id);

            if (product == null)
            { 
                TempData["ErrorMessage"] = "Product not found.";
                return RedirectToAction("Index");
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                bool deleted = _productService.DeleteProduct(id);

                if (deleted)
                {
                    TempData["SuccessMessage"] = "Product successfully deleted.";
                }

                if (!deleted)
                {
                    TempData["ErrorMessage"] = "Deleting the product failed.";
                }
            }
            catch
            {
                TempData["ErrorMessage"] = "Deleting the product failed";
            }

            return RedirectToAction("Index");
        }
    }
}