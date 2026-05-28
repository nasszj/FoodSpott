using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLibrary.Services;
using UnitTest.FakeRepositories;

namespace UnitTest.ServiceTesten
{
    [TestClass]
    public class CartServiceTest
    {
        [TestMethod]
        public void GetCart_ReturnsCartWithProducts()
        {
            //Arrange
            var service = new CartService(new FakeCartRepository());

            //Act
            var result = service.GetCart();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Products.Count);
        }

        [TestMethod]
        public void AddProduct_AddsProductToCart()
        {
            //Arrange
            var service = new CartService(new FakeCartRepository());

            //Act
            service.AddProduct(3);
            var result = service.GetCart();

            //Assert
            Assert.AreEqual(3, result.Products.Count);
        }

        [TestMethod]
        public void UpdateQuantity_UpdatesQuantity_WhenValid()
        {
            //Arrange
            var service = new CartService(new FakeCartRepository());

            //Act
            service.UpdateQuantity(1, 2);
            var result = service.GetCart();

            //Assert
            Assert.AreEqual(2, result.Products[0].Quantity);
            Assert.AreEqual(12.00m, result.Products[0].Subtotal);
        }

        [TestMethod]
        public void DeleteProduct_ReturnsTrue_WhenProductExists()
        {
            //Arrange
            var service = new CartService(new FakeCartRepository());

            //Act
            var result = service.DeleteProduct(1);

            //Assert
            Assert.IsTrue(result);
        }
    }
}