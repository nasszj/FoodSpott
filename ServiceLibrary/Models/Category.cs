using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLibrary.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }

        public Category()
        { 
        }

        public Category(int categoryID, string name)
        {
            CategoryID = categoryID;
            Name = name;
        }
    }
}
