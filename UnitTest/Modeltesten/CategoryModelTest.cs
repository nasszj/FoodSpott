using System;
using System.Collections.Generic;
using System.Text;
using ServiceLibrary.Models;

namespace UnitTest.Modeltesten
{
    [TestClass]
    public class CategoryModelTest
    {
        [TestMethod]
        public void Category_Constructot_SetsPropertiesCorrectly()
        {
            //Arrange
            var categoryID = 1;
            var name = "Pizza";

            //Act
            var category = new Category(categoryID, name);

            //Assert
            Assert.AreEqual(categoryID, category.CategoryID);
            Assert.AreEqual(name, category.Name);
        }
    }
}
