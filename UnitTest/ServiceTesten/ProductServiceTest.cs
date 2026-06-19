using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLibrary.Models;
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
            // Arrange
            var service = new ProductService(new FakeProductRepository());

            // Act
            var result = service.GetAllProducts();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count);
        }

        [TestMethod]
        public void GetAllProducts_ReturnsEmptyList_WhenNoProductsAvailable()
        {
            // Arrange
            var service = new ProductService(new FakeProductRepository(true));

            // Act
            var result = service.GetAllProducts();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetProductById_ReturnsCorrectProduct()
        {
            // Arrange
            var service = new ProductService(new FakeProductRepository());

            // Act
            var result = service.GetProductById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Cheeseburger", result.Name);
            Assert.AreEqual(6.00m, result.Price);
        }

        [TestMethod]
        public void GetProductById_ReturnsNull_WhenProductDoesNotExist()
        {
            // Arrange
            var service = new ProductService(new FakeProductRepository());

            // Act
            var result = service.GetProductById(99);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void AddProduct_AddsProduct_WhenProductIsValid()
        {
            // Arrange
            var service = new ProductService(new FakeProductRepository());

            Product product = new Product(
                5,
                "Chicken Nuggets",
                4.50m,
                "Crispy chicken nuggets",
                5,
                "/images/products/chicken nuggets.jpg"
            );

            // Act
            service.AddProduct(product);
            var result = service.GetAllProducts();

            // Assert
            Assert.AreEqual(5, result.Count);
            Assert.AreEqual("Chicken Nuggets", result[4].Name);
        }

        [TestMethod]
        public void AddProduct_DoesNotAddProduct_WhenNameOrPriceIsInvalid()
        {
            // Arrange
            var service = new ProductService(new FakeProductRepository());

            Product product = new Product(
                5,
                "",
                0,
                "Invalid product",
                5,
                "/images/products/invalid.jpg"
            );

            // Act
            service.AddProduct(product);
            var result = service.GetAllProducts();

            // Assert
            Assert.AreEqual(4, result.Count);
        }

        [TestMethod]
        public void UpdateProduct_UpdatesProduct_WhenProductIsValid()
        {
            // Arrange
            var service = new ProductService(new FakeProductRepository());

            Product product = new Product(
                1,
                "Updated Cheeseburger",
                6.50m,
                "Updated burger description",
                2,
                "/images/products/cheeseburger.jpg"
            );

            // Act
            service.UpdateProduct(product);
            var result = service.GetProductById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Updated Cheeseburger", result.Name);
            Assert.AreEqual(6.50m, result.Price);
        }

        [TestMethod]
        public void UpdateProduct_DoesNotUpdateProduct_WhenNameOrPriceIsInvalid()
        {
            // Arrange
            var service = new ProductService(new FakeProductRepository());

            Product product = new Product(
                1,
                "",
                0,
                "Invalid update",
                2,
                "/images/products/cheeseburger.jpg"
            );

            // Act
            service.UpdateProduct(product);
            var result = service.GetProductById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Cheeseburger", result.Name);
            Assert.AreEqual(6.00m, result.Price);
        }

        [TestMethod]
        public void DeleteProduct_ReturnsTrue_WhenProductExists()
        {
            // Arrange
            var service = new ProductService(new FakeProductRepository());

            // Act
            var result = service.DeleteProduct(1);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteProduct_ReturnsFalse_WhenProductDoesNotExist()
        {
            // Arrange
            var service = new ProductService(new FakeProductRepository());

            // Act
            var result = service.DeleteProduct(99);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetAllProducts_ReturnsProducts_WhenCategoryExists()
        {
            // Arrange
            var service = new ProductService(new FakeProductRepository());

            // Act
            var result = service.GetAllProducts("Burgers");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Cheeseburger", result[0].Name);
        }

        [TestMethod]
        public void GetAllProducts_ReturnsEmptyList_WhenCategoryHasNoProducts()
        {
            // Arrange
            var service = new ProductService(new FakeProductRepository());

            // Act
            var result = service.GetAllProducts("Snacks");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }
    }
}