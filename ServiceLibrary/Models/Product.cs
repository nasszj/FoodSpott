using ServiceLibrary.Models.Mappers;

namespace ServiceLibrary.Models;

    public class Product
{
    public int ProductID { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }

    public Product()
    {
        
    }

    public Product(int productID, string name, decimal price, string description)
    {
        ProductID = productID;
        Name = name;
        Price = price;
        Description = description;
    }
}

