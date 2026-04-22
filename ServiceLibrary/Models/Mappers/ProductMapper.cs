using DAL;
using Interfaces;
using ServiceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLibrary.Models.Mappers
{
    public class ProductMapper
    {
        static public Product ProductModelFromDto(ProductDTO dto) 
        {
            return new Product(
                dto.ProductID,
                dto.Name,
                dto.Price,
                dto.Description,
                dto.CategoryID
            );
        }

        static public ProductDTO ProductDTOFromModel(Product product)
        {
            return new ProductDTO
            {
                ProductID = product.ProductID,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryID = product.CategoryID,
            };
        }
    }
}
