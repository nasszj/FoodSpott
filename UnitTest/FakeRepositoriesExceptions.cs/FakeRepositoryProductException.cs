using Interfaces;
using System.Collections.Generic;

namespace UnitTest.FakeRepositories
{
    public class FakeRepositoryProductException : IProductRepository
    {
        public List<ProductDTO> GetAllProducts(string category = "")
        {
            throw new Exception("Products could not be loaded.");
        }

        public ProductDTO GetProductById(int id)
        {
            throw new Exception("Products could not be loaded.");
        }

        public void AddProduct(ProductDTO product)
        {
            throw new Exception("Product could not be added.");
        }

        public void UpdateProduct(ProductDTO product)
        {
            throw new Exception("Product could not be updated.");
        }

        public bool DeleteProduct(int id)
        {
            throw new Exception("Product could not be deleted.");
        }
    }
}