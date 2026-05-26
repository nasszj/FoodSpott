using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLibrary.Models
{
    public class Cart
    {
        public int CartID { get; set; }
        public int? UserID { get; set; }
        public decimal TotalPrice { get; set; }
        public List<CartProduct> Products { get; set; } = new List<CartProduct>();
    }
}
