using DAL.Repositories;
using ServiceLibrary.Models;

namespace ServiceLibrary.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> GetAllProducts()
        {
            // Convert ProductDTO to Product
            return _productRepository.GetAllProducts();
            List<Product> products = new List<Product>();
            foreach (var productDTO in _productRepository.GetAllProducts()) ;
            {
                Product product = new Product();
                {
                    ProductID = dto
                }
            }
        }

        public Product GetProductById(int id)
        {
            return _productRepository.GetProductById(id);
        }

        public void AddProduct(Product product)
        {
            _productRepository.AddProduct(product);
        }
    }
}