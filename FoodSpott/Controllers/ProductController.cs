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

        private bool IsAdmin()
        {
            return HttpContext.Session.GetString("Role") == "Admin";
        }

        public IActionResult Index(string category)
        {
            try
            {
                List<Product> products = _productService.GetAllProducts(category);
                ViewBag.CurrentCategory = category;
                return View(products);
            }
            catch
            {
                TempData["ErrorMessage"] = "Products could not be loaded. Please try again later.";
                return View(new List<Product>());
            }
        }


        public IActionResult Details(int id)
        {
            try
            { 
                Product product = _productService.GetProductById(id);

                if (product == null)
                {
                    return NotFound(); 
                }

                return View(product);
            }

            catch
            {
                TempData["ErrorMessage"] = "Categories could not be loaded. Please try again later.";
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public IActionResult Create()
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index");
            }

            try
            { 
                ProductViewModel vm = new ProductViewModel
                {
                    ProductID = 0,
                    Name = "",
                    Price = 0,
                    Description = "",
                    CategoryID = 0,
                    Categories = _categoryService.GetAllCategories()
                };

                return View(vm);
            }

            catch
            {
                TempData["ErrorMessage"] = "Categories could not be loaded. Please try again later.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel vm)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                vm.Categories = _categoryService.GetAllCategories();
                return View(vm);
            }

            try
            {
                Product product = new Product(
                    vm.ProductID,
                    vm.Name,
                    vm.Price,
                    vm.Description,
                    vm.CategoryID,
                    vm.ImagePath
                );

                _productService.AddProduct(product);

                TempData["SuccessMessage"] = "Product successfully added.";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorMessage"] = "Product could not be added. Please try again later.";
                vm.Categories = _categoryService.GetAllCategories();
                return View(vm);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index");
            }

            try
            { 
                Product product = _productService.GetProductById(id);

                if (product == null)
                {
                    return NotFound();
                }

                ProductViewModel vm = new ProductViewModel
                {
                    ProductID = product.ProductID,
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                    CategoryID = product.CategoryID,
                    Categories = _categoryService.GetAllCategories()
                };

                return View(vm);
            }

            catch
            {
                TempData["ErrorMessage"] = "Product data could not be loaded. Please try again later.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel vm)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index");
            }

            if (vm == null)
            {
                TempData["ErrorMessage"] = "Product data is missing.";
                vm.Categories = _categoryService.GetAllCategories();
                return View(vm);
            }

            if (string.IsNullOrWhiteSpace(vm.Name) || vm.Price <= 0)
            {
                TempData["ErrorMessage"] = "Name and price are required.";
                vm.Categories = _categoryService.GetAllCategories();
                return View(vm);
            }

            try
            {
                Product product = new Product(
                    vm.ProductID,
                    vm.Name,
                    vm.Price,
                    vm.Description,
                    vm.CategoryID,
                    vm.ImagePath
                );

                _productService.UpdateProduct(product);

                TempData["SuccessMessage"] = "Product successfully updated.";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorMessage"] = "Product could not be updated. Please try again later.";
                vm.Categories = _categoryService.GetAllCategories();
                return View(vm);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index");
            }

            try
            { 
                Product product = _productService.GetProductById(id);

                if (product == null)
                { 
                    TempData["ErrorMessage"] = "Product not found.";
                    return RedirectToAction("Index");
                }

                return View(product);
            }

            catch
            {
                TempData["ErrorMessage"] = "Something went wrong. Please try again later.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index");
            }

            try
            {
                bool deleted = _productService.DeleteProduct(id);

                if (deleted)
                {
                    TempData["SuccessMessage"] = "Product successfully deleted.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Product could not be deleted. Please try again later.";
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