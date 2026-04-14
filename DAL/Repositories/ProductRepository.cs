namespace DAL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

public class ProductRepository
{
    private readonly string _connectionString;

    public ProductRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection");
    }

    public List<ProductDTO> GetAllProducts()
    {
        List<ProductDTO> products = new List<ProductDTO>();

        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        string query = "SELECT ProductID, Name, Price, Description FROM Product";

        using SqlCommand command = new SqlCommand(query, connection);
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            ProductDTO product = new ProductDTO
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

    public ProductDTO GetProductById(int id)
    {
        ProductDTO product = null;
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        string query = "SELECT ProductID, Name, Price, Description FROM Product WHERE ProductID = @id";
        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);
        using SqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            product = new ProductDTO
            {
                ProductID = Convert.ToInt32(reader["ProductID"]),
                Name = reader["Name"].ToString(),
                Price = Convert.ToDecimal(reader["Price"]),
                Description = reader["Description"].ToString(),
            };
        }
        return product;
    }

    public void AddProduct(ProductDTO product)
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