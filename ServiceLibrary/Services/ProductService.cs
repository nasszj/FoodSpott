using DAL;
using Interfaces;
using Interfaces.Interface;
using ServiceLibrary.Models;
using ServiceLibrary.Models.Mappers;

namespace ServiceLibrary.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> GetAllProducts(string category = "")
        {
            // Convert ProductDTO to Product
            List<Product> products = new List<Product>();
            foreach (ProductDTO productDTO in _productRepository.GetAllProducts(category)) 
            {
                products.Add(new Product(productDTO.ProductID,productDTO.Name,productDTO.Price,productDTO.Description,productDTO.Category));
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

        public void UpdateProduct(Product product)
        {
            ProductDTO dto = ProductMapper.ProductDTOFromModel(product);
            _productRepository.UpdateProduct(dto);
        }

        public bool DeleteProduct(int id)
        {
            return _productRepository.DeleteProduct(id);
        }
    }
}