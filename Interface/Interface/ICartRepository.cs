using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Interface
{
    public interface ICartRepository
    {
        CartDTO GetCart();
        void AddProduct(int productID);
        void UpdateQuantity(int cartProductID, int quantity);
        bool DeleteProduct(int cartProductID);
    }
}
