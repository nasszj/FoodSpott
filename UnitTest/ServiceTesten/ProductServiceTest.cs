using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLibrary.Services;
using UnitTest.FakeRepositories;

namespace UnitTest.ServiceTesten
{
    [TestClass]
    public class ProductServiceTest
    {
        [TestMethod]
        public void GetAllProducts_ReturnsAllProducts()
        {
            //Arrange
            var service = new ProductService(new FakeProductRepository());

            //Act
            var result = service.GetAllProducts();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void GetProductById_ReturnsCorrectProduct()
        {
            //Arrange
            var service = new ProductService(new FakeProductRepository());

            //Act
            var result = service.GetProductById(1);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Cheeseburger", result.Name);
        }

        [TestMethod]
        public void GetProductById_ReturnsNull_WhenNotFound()
        {
            //Arrange
            var service = new ProductService(new FakeProductRepository());

            //Act
            var result = service.GetProductById(99);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DeleteProduct_ReturnsTrue_WhenProductExists()
        {
            //Arrange
            var service = new ProductService(new FakeProductRepository());

            //Act
            var result = service.DeleteProduct(1);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
