using System.Collections.Generic;
using ServiceLibrary.Models;

namespace FoodSpott.Models
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public string ImagePath { get; set; }
        public List<Category> Categories { get; set; }
    }
}