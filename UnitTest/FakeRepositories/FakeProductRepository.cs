using Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest.FakeRepositories
{
    public class FakeProductRepository : IProductRepository
    {
        private readonly List<ProductDTO> _products;

        public FakeProductRepository()
        {
            _products = new List<ProductDTO>
            {
                new ProductDTO { ProductID = 1, Name = "Cheeseburger", Price = 6.00m, Description = "Beef burger with cheese", CategoryID = 2, ImagePath = "/images/products/cheeseburger.jpg" },
                new ProductDTO { ProductID = 2, Name = "Fries", Price = 3.95m, Description = "French fries", CategoryID = 3, ImagePath = "/images/products/fries.jpg" },
                new ProductDTO { ProductID = 3, Name = "Coca Cola", Price = 2.50m, Description = "Soft drink", CategoryID = 4, ImagePath = "/images/products/cola.jpg" },
                new ProductDTO { ProductID = 4, Name = "Pizza Margherita", Price = 8.90m, Description = "Classic pizza with tomato and mozzarella", CategoryID = 1, ImagePath = "/images/products/pizza margherita.jpg" }
            };
        }

        public FakeProductRepository(bool emptyList)
        {
            _products = emptyList
                ? new List<ProductDTO>()
                : new List<ProductDTO>
                {
                    new ProductDTO { ProductID = 1, Name = "Cheeseburger", Price = 6.00m, Description = "Beef burger with cheese", CategoryID = 2, ImagePath = "/images/products/cheeseburger.jpg" },
                    new ProductDTO { ProductID = 2, Name = "Fries", Price = 3.95m, Description = "French fries", CategoryID = 3, ImagePath = "/images/products/fries.jpg" },
                    new ProductDTO { ProductID = 3, Name = "Coca Cola", Price = 2.50m, Description = "Soft drink", CategoryID = 4, ImagePath = "/images/products/cola.jpg" },
                    new ProductDTO { ProductID = 4, Name = "Pizza Margherita", Price = 8.90m, Description = "Classic pizza with tomato and mozzarella", CategoryID = 1, ImagePath = "/images/products/pizza margherita.jpg" }
                };
        }

        public List<ProductDTO> GetAllProducts(string category = "")
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return _products;
            }

            if (category == "Pizza")
            {
                return _products.Where(p => p.CategoryID == 1).ToList();
            }

            if (category == "Burgers")
            {
                return _products.Where(p => p.CategoryID == 2).ToList();
            }

            if (category == "Fries")
            {
                return _products.Where(p => p.CategoryID == 3).ToList();
            }

            if (category == "Drinks")
            {
                return _products.Where(p => p.CategoryID == 4).ToList();
            }

            if (category == "Snacks")
            {
                return _products.Where(p => p.CategoryID == 5).ToList();
            }

            return new List<ProductDTO>();
        }

        public ProductDTO GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.ProductID == id);
        }

        public void AddProduct(ProductDTO product)
        {
            if (string.IsNullOrWhiteSpace(product.Name) || product.Price <= 0)
            {
                return;
            }

            _products.Add(product);
        }

        public void UpdateProduct(ProductDTO product)
        {
            if (string.IsNullOrWhiteSpace(product.Name) || product.Price <= 0)
            {
                return;
            }

            ProductDTO existingProduct = _products.FirstOrDefault(p => p.ProductID == product.ProductID);

            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;
                existingProduct.CategoryID = product.CategoryID;
                existingProduct.ImagePath = product.ImagePath;
            }
        }

        public bool DeleteProduct(int id)
        {
            ProductDTO product = _products.FirstOrDefault(p => p.ProductID == id);

            if (product == null)
            {
                return false;
            }

            _products.Remove(product);
            return true;
        }
    }
}