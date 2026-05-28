using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLibrary.Services;
using UnitTest.FakeRepositories;

namespace UnitTest.ServiceTesten
{
    [TestClass]
    public class CategoryServiceTest
    {
        [TestMethod]
        public void GetAllCategories_ReturnsAllCategories()
        {
            //Arrange
            var service = new CategoryService(new FakeCategoryRepository());

            //Act
            var result = service.GetAllCategories();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count);
        }
    }
}