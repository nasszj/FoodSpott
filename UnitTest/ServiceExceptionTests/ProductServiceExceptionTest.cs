using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLibrary.Services;
using UnitTest.FakeRepositories;

namespace UnitTest.ServiceTesten
{
    [TestClass]
    public class ProductServiceExceptionTest
    {
        [TestMethod]
        public void GetProductById_ReturnsNull_WhenProductDoesNotExist()
        {
            //Arrange
            var service = new ProductService(new FakeProductRepository());

            //Act
            var result = service.GetProductById(99);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DeleteProduct_ReturnsFalse_WhenProductDoesNotExist()
        {
            //Arrange
            var service = new ProductService(new FakeProductRepository());

            //Act
            var result = service.DeleteProduct(99);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetAllProducts_ReturnsEmptyList_WhenCategoryHasNoProducts()
        {
            //Arrange
            var service = new ProductService(new FakeProductRepository());

            //Act
            var result = service.GetAllProducts("Snacks");

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }
    }
}