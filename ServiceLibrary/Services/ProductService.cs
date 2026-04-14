using DAL;
using DAL.Repositories;
using ServiceLibrary.Models;
using ServiceLibrary.Models.Mappers;

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
            List<Product> products = new List<Product>();
            foreach (ProductDTO productDTO in _productRepository.GetAllProducts()) 
            {
                products.Add(new Product(productDTO.ProductID,productDTO.Name,productDTO.Price,productDTO.Description));
            }
            return products;
        }


        public Product GetProductById(int id)
        {
            return ProductMapper.ProductModelFromDto(_productRepository.GetProductById(id));
        }

        public void AddProduct(Product product)
        {
            ProductDTO dto = ProductMapper.ProductDTOFromModel(product);
            _productRepository.AddProduct(dto);
        }
    }
}