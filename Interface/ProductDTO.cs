using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public class ProductDTO
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
    }
}
