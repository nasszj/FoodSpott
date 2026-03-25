namespace FoodSpott.Repositories;

using FoodSpott.Models;
using Microsoft.Data.SqlClient;

public class ProductRepository
{
    private readonly string _connectionString;

    public ProductRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection");
    }

    public List<Product> GetAllProducts()
    {
        List<Product> products = new List<Product>();

        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        string query = "SELECT ProductID, Name, Price, Description FROM Product";

        using SqlCommand command = new SqlCommand(query, connection);
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Product product = new Product
            {
                ProductID = Convert.ToInt32(reader["ProductID"]),
                Name = reader["Name"].ToString(),
                Price = Convert.ToDecimal(reader["Price"]),
                Description = reader["Description"].ToString(),
            };

            products.Add(product);
        }

        return products;
    }

    public Product GetProductById(int id)
    {
        Product product = null;
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        string query = "SELECT ProductID, Name, Price, Description FROM Product WHERE ProductID = @id";
        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);
        using SqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            product = new Product
            {
                ProductID = Convert.ToInt32(reader["ProductID"]),
                Name = reader["Name"].ToString(),
                Price = Convert.ToDecimal(reader["Price"]),
                Description = reader["Description"].ToString(),
            };
        }
        return product;
    }

    public void AddProduct(Product product)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        string query = "INSERT INTO product (Name, Price, Description) VALUES (@Name, @Price, @Description)";

        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Name", product.Name);
        command.Parameters.AddWithValue("@Price", product.Price);
        command.Parameters.AddWithValue("@Description", product.Description ?? (object)DBNull.Value);

        command.ExecuteNonQuery();
    }
}