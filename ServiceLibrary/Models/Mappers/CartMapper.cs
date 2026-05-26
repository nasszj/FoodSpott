using Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ServiceLibrary.Models.Mappers
{
    public class CartMapper
    {
        static public Cart CartModelFromDto(CartDTO dto)
        {
            List<CartProduct> products = new List<CartProduct>();

            foreach (CartProductDTO productDTO in dto.Products)
            {
                products.Add(CartProductModelFromDto(productDTO));
            }

            return new Cart
            {
                CartID = dto.CartID,
                UserID = dto.UserID,
                TotalPrice = dto.TotalPrice,
                Products = products
            };
        }

        static public CartProduct CartProductModelFromDto(CartProductDTO dto)
        {
            return new CartProduct
            {
                CartProductID = dto.CartProductID,
                CartID = dto.CartID,
                ProductID = dto.ProductID,
                ProductName = dto.ProductName,
                Price = dto.Price,
                Quantity = dto.Quantity,
                Subtotal = dto.Subtotal
            };
        }
    }
}