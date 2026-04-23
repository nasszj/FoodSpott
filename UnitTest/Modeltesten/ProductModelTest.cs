using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLibrary.Models;

namespace UnitTest.ModelTesten
{
    [TestClass]
    public class ProductTest
    {
        [TestMethod]
        public void Product_Constructor_SetsPropertiesCorrectly()
        {
            //Arrange
            var productID = 1;
            var name = "Cheeseburger";
            var price = 6.00m;
            var description = "Beef burger with cheese";
            var categoryID = 2;

            //Act
            var product = new Product(productID, name, price, description, categoryID);

            //Assert
            Assert.AreEqual(productID, product.ProductID);
            Assert.AreEqual(name, product.Name);
            Assert.AreEqual(price, product.Price);
            Assert.AreEqual(description, product.Description);
            Assert.AreEqual(categoryID, product.CategoryID);
        }
    }
}