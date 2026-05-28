using Interfaces;
using Interfaces.Interface;

namespace UnitTest.FakeRepositories
{
    public class FakeRepositoryCartException : ICartRepository
    {
        public CartDTO GetCart()
        {
            throw new Exception("Cart could not be loaded.");
        }

        public void AddProduct(int productID)
        {
            throw new Exception("Product could not be added to cart.");
        }

        public void UpdateQuantity(int cartProductID, int quantity)
        {
            throw new Exception("Cart quantity could not be updated.");
        }

        public bool DeleteProduct(int cartProductID)
        {
            throw new Exception("Product could not be removed from cart.");
        }
    }
}