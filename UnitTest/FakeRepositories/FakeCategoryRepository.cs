using Interfaces;
using Interfaces.Interface;
using System.Collections.Generic;

namespace UnitTest.FakeRepositories
{
    public class FakeCategoryRepository: ICategoryRepository
    {
        public List<CategoryDTO> GetAllCategories()
        {
            return new List<CategoryDTO>
            {
                new CategoryDTO { CategoryID = 1, Name = "Pizza" },
                new CategoryDTO { CategoryID = 2, Name = "Burgers" },
                new CategoryDTO { CategoryID = 3, Name = "Fries" },
                new CategoryDTO { CategoryID = 4, Name = "Drinks" }
            };
        }
    }
}