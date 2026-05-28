using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLibrary.Services;
using UnitTest.FakeRepositories;

namespace UnitTest.ServiceTesten
{
    [TestClass]
    public class CartServiceExceptionTest
    {
        [TestMethod]
        public void UpdateQuantity_DoesNotUpdate_WhenQuantityIsLowerThanOne()
        {
            //Arrange
            var service = new CartService(new FakeCartRepository());

            //Act
            service.UpdateQuantity(1, 0);
            var result = service.GetCart();

            //Assert
            Assert.AreEqual(1, result.Products[0].Quantity);
            Assert.AreEqual(6.00m, result.Products[0].Subtotal);
        }

        [TestMethod]
        public void DeleteProduct_UpdatesTotalPrice_AfterProductIsRemoved()
        {
            //Arrange
            var service = new CartService(new FakeCartRepository());

            //Act
            service.DeleteProduct(1);
            var result = service.GetCart();

            //Assert
            Assert.AreEqual(3.95m, result.TotalPrice);
            Assert.AreEqual(1, result.Products.Count);
        }
    }
}