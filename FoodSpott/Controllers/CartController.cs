using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using ServiceLibrary.Models;
using ServiceLibrary.Services;

namespace FoodSpott.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;

        public CartController(IConfiguration configuration)
        {
            _cartService = new CartService(new CartRepository(configuration));
        }

        public IActionResult Index()
        {
            Cart cart = _cartService.GetCart();

            if (cart == null)
            {
                cart = new Cart
                {
                    CartID = 0,
                    UserID = null,
                    TotalPrice = 0,
                    Products = new List<CartProduct>()
                };
            }

            return View(cart);
        }

        public IActionResult AddProduct(int productID)
        {
            try
            {
                _cartService.AddProduct(productID);
                TempData["SuccessMessage"] = "Product added to cart.";
            }
            catch
            {
                TempData["ErrorMessage"] = "Adding product to cart failed.";
            }

            return RedirectToAction("Index", "Product");
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int cartProductID, int quantity)
        {
            if (quantity < 1)
            {
                TempData["ErrorMessage"] = "Quantity cannot be lower than 1.";
                return RedirectToAction("Index");
            }

            try
            {
                _cartService.UpdateQuantity(cartProductID, quantity);
                TempData["SuccessMessage"] = "Cart updated.";
            }
            catch
            {
                TempData["ErrorMessage"] = "Updating cart failed.";
            }

            return RedirectToAction("Index");
        }

        public IActionResult DeleteProduct(int cartProductID)
        {
            try
            {
                bool deleted = _cartService.DeleteProduct(cartProductID);

                if (deleted)
                {
                    TempData["SuccessMessage"] = "Product removed from cart.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Removing the product failed.";
                }
            }
            catch
            {
                TempData["ErrorMessage"] = "Removing the product failed.";
            }

            return RedirectToAction("Index");
        }
    }
}