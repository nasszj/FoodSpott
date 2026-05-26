using DAL;
using Interfaces;
using Interfaces.Interface;
using ServiceLibrary.Models;
using ServiceLibrary.Models.Mappers;

namespace ServiceLibrary.Services
{
    public class CartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public Cart GetCart()
        {
            CartDTO dto = _cartRepository.GetCart();

            if (dto == null)
            {
                return null;
            }

            return CartMapper.CartModelFromDto(dto);
        }

        public void AddProduct(int productID)
        {
            _cartRepository.AddProduct(productID);
        }

        public void UpdateQuantity(int cartProductID, int quantity)
        {
            _cartRepository.UpdateQuantity(cartProductID, quantity);
        }

        public bool DeleteProduct(int cartProductID)
        {
            return _cartRepository.DeleteProduct(cartProductID);
        }
    }
}