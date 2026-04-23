using DAL.Repositories;
using FoodSpott.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ServiceLibrary.Models;
using ServiceLibrary.Services;

namespace FoodSpott.Controllers
{
    public class ProductController : Controller

    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;

        public ProductController(IConfiguration configuration)
        {
            _productService = new ProductService(new ProductRepository(configuration));
            _categoryService = new CategoryService(new CategoryRepository(configuration));
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
            ProductViewModel vm = new ProductViewModel
            {
                Product = new Product(0, "", 0, "", 0),
                Categories = _categoryService.GetAllCategories()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Categories = _categoryService.GetAllCategories();
                return View(vm);
            }

            try
            {
                _productService.AddProduct(vm.Product);
                TempData["SuccessMessage"] = "Product successfully added.";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorMessage"] = "Adding the product failed.";
                vm.Categories = _categoryService.GetAllCategories();
                return View(vm);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Product product = _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            ProductViewModel vm = new ProductViewModel
            {
                Product = product,
                Categories = _categoryService.GetAllCategories()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel vm)
        {
            if (vm.Product == null)
            {
                TempData["ErrorMessage"] = "Product data is missing.";
                vm.Categories = _categoryService.GetAllCategories();
                return View(vm);
            }

            if (string.IsNullOrWhiteSpace(vm.Product.Name) || vm.Product.Price <= 0)
            {
                TempData["ErrorMessage"] = "Name and price are required.";
                vm.Categories = _categoryService.GetAllCategories();
                return View(vm);
            }

            try
            {
                _productService.UpdateProduct(vm.Product);
                TempData["SuccessMessage"] = "Product successfully updated.";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorMessage"] = "Updating the product failed.";
                vm.Categories = _categoryService.GetAllCategories();
                return View(vm);
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
                else
                {
                    TempData["ErrorMessage"] = "Deleting the product failed.";
                }
            }
            catch
            {
                TempData["ErrorMessage"] = "Deleting the product failed.";
            }

            return RedirectToAction("Index");
        }
    }
}