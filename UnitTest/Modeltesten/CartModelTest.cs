using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLibrary.Models;
using System.Collections.Generic;

namespace UnitTest.ModelTesten
{
    [TestClass]
    public class CartTest
    {
        [TestMethod]
        public void Cart_Properties_SetCorrectly()
        {
            //Arrange
            var cartID = 1;
            int? userID = 1;
            var totalPrice = 9.95m;

            var products = new List<CartProduct>
            {
                new CartProduct { CartProductID = 1, CartID = 1, ProductID = 1, ProductName = "Cheeseburger", Price = 6.00m, Quantity = 1, Subtotal = 6.00m }
            };

            //Act
            var cart = new Cart
            {
                CartID = cartID,
                UserID = userID,
                TotalPrice = totalPrice,
                Products = products
            };

            //Assert
            Assert.AreEqual(cartID, cart.CartID);
            Assert.AreEqual(userID, cart.UserID);
            Assert.AreEqual(totalPrice, cart.TotalPrice);
            Assert.AreEqual(1, cart.Products.Count);
        }
    }
}