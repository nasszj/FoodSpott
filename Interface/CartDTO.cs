using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public class CartDTO
    {
        public int CartID { get; set; }
        public int? UserID { get; set; }
        public decimal TotalPrice { get; set; }
        public List<CartProductDTO> Products { get; set; } = new List<CartProductDTO>();
    }
}
