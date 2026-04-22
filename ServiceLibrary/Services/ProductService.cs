using DAL;
using Interfaces;
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
                products.Add(ProductMapper.ProductModelFromDto(productDTO));
            }
            return products;
        }


        public Product GetProductById(int id)
        {
            var dto = _productRepository.GetProductById(id);

            if (dto == null)
            {
                return null;
            }

            return ProductMapper.ProductModelFromDto(dto);
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