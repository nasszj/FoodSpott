using DAL;
using DAL.Repositories;
using Interfaces;
using ServiceLibrary.Models;

namespace ServiceLibrary.Services
{
    public class CategoryService
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryService(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();

            foreach (CategoryDTO categoryDTO in _categoryRepository.GetAllCategories())
            {
                categories.Add(new Category
                {
                    CategoryID = categoryDTO.CategoryID,
                    Name = categoryDTO.Name
                });
            }

            return categories;
        }
    }
}