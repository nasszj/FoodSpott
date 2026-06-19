using Interfaces;
using Interfaces.Interface;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest.FakeRepositories
{
    public class FakeCartRepository : ICartRepository
    {
        private List<CartProductDTO> products;

        public FakeCartRepository(bool emptyCart = false)
        {
            if (emptyCart)
            {
                products = new List<CartProductDTO>();
            }
            else
            {
                products = new List<CartProductDTO>
        {
            new CartProductDTO { CartProductID = 1, CartID = 1, ProductID = 1, ProductName = "Cheeseburger", Price = 6.00m, Quantity = 1, Subtotal = 6.00m },
            new CartProductDTO { CartProductID = 2, CartID = 1, ProductID = 2, ProductName = "Fries", Price = 3.95m, Quantity = 1, Subtotal = 3.95m }
        };
            }
        }

        public CartDTO GetCart()
        {
            return new CartDTO
            {
                CartID = 1,
                UserID = 1,
                TotalPrice = products.Sum(p => p.Subtotal),
                Products = products
            };
        }

        public void AddProduct(int productID)
        {
            if (productID == 3)
            {
                products.Add(new CartProductDTO
                {
                    CartProductID = 3,
                    CartID = 1,
                    ProductID = 3,
                    ProductName = "Coca Cola",
                    Price = 2.50m,
                    Quantity = 1,
                    Subtotal = 2.50m
                });
            }
        }

        public void UpdateQuantity(int cartProductID, int quantity)
        {
            CartProductDTO product = products.FirstOrDefault(p => p.CartProductID == cartProductID);

            if (product != null && quantity >= 1)
            {
                product.Quantity = quantity;
                product.Subtotal = product.Price * quantity;
            }
        }

        public bool DeleteProduct(int cartProductID)
        {
            CartProductDTO product = products.FirstOrDefault(p => p.CartProductID == cartProductID);

            if (product != null)
            {
                products.Remove(product);
                return true;
            }

            return false;
        }
    }
}