using Interfaces;
using System.Collections.Generic;


namespace UnitTest.FakeRepositories
{
    public class FakeProductRepository : IProductRepository
    {
        public List<ProductDTO> GetAllProducts(string category = "")
        {
            List<ProductDTO> products = new List<ProductDTO>
            {
                new ProductDTO { ProductID = 1, Name = "Cheeseburger", Price = 6.00m, Description = "Beef burger with cheese", CategoryID = 2 },
                new ProductDTO { ProductID = 2, Name = "Fries", Price = 3.95m, Description = "French fries", CategoryID = 3 },
                new ProductDTO { ProductID = 3, Name = "Coca Cola", Price = 2.50m, Description = "Soft drink", CategoryID = 4 }
            };

            return products;
        }

            public ProductDTO GetProductById(int id)
            {
                if (id == 1)
                {
                    return new ProductDTO 
                    {
                        ProductID = 1, 
                        Name = "Cheeseburger",
                        Price = 6.00m,
                        Description = "Beef burger with cheese",
                        CategoryID = 2 
                    };
                }

                if (id == 2)
                {
                    return new ProductDTO
                    { 
                        ProductID = 2,
                        Name = "Fries",
                        Price = 3.95m,
                        Description = "French fries",
                        CategoryID = 3 
                    };
                }

                return null;
            }

            public void AddProduct(ProductDTO product)
            {
            }

            public void UpdateProduct(ProductDTO product)
            {
            }

            public bool DeleteProduct(int id)
            {
                return id == 1;
            }
    }
}
