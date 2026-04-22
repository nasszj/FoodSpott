using ServiceLibrary.Models.Mappers;

namespace ServiceLibrary.Models;

    public class Product
{
    public int ProductID { get; }
    public string Name { get; }
    public decimal Price { get; }
    public string Description { get; }
    public int CategoryID { get; }

    public Product(int productID, string name, decimal price, string description, int categoryID)
    {
        ProductID = productID;
        Name = name;
        Price = price;
        Description = description;
        CategoryID = categoryID;
    }
}

