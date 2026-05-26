using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public class CartProductDTO
    {
        public int CartProductID { get; set; }
        public int CartID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
    }
}
