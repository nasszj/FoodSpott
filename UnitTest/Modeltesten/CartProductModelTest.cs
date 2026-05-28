using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLibrary.Models;

namespace UnitTest.ModelTesten
{
    [TestClass]
    public class CartProductTest
    {
        [TestMethod]
        public void CartProduct_Properties_SetCorrectly()
        {
            //Arrange
            var cartProductID = 1;
            var cartID = 1;
            var productID = 1;
            var productName = "Cheeseburger";
            var price = 6.00m;
            var quantity = 2;
            var subtotal = 12.00m;

            //Act
            var cartProduct = new CartProduct
            {
                CartProductID = cartProductID,
                CartID = cartID,
                ProductID = productID,
                ProductName = productName,
                Price = price,
                Quantity = quantity,
                Subtotal = subtotal
            };

            //Assert
            Assert.AreEqual(cartProductID, cartProduct.CartProductID);
            Assert.AreEqual(cartID, cartProduct.CartID);
            Assert.AreEqual(productID, cartProduct.ProductID);
            Assert.AreEqual(productName, cartProduct.ProductName);
            Assert.AreEqual(price, cartProduct.Price);
            Assert.AreEqual(quantity, cartProduct.Quantity);
            Assert.AreEqual(subtotal, cartProduct.Subtotal);
        }
    }
}