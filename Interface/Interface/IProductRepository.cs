using System;
using System.Collections.Generic;
using System.Text;


namespace Interfaces
{
    public interface IProductRepository
    {
        List<ProductDTO> GetAllProducts(string category = "");  
        ProductDTO GetProductById(int id);
        void AddProduct(ProductDTO product);
        void UpdateProduct(ProductDTO product);
        bool DeleteProduct(int id);
    }
}
