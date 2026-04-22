using System.Collections.Generic;
using ServiceLibrary.Models;

namespace FoodSpott.Models
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
    }
}