namespace DAL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Interfaces;

public class ProductRepository : IProductRepository
{
    private readonly string _connectionString;

    public ProductRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection");
    }

    public List<ProductDTO> GetAllProducts(string category = "")
    {
        List<ProductDTO> products = new List<ProductDTO>();

        try
        {

            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"
            SELECT p.ProductID, p.Name, p.Price, p.Description, p.CategoryID
            FROM Product p
            INNER JOIN Category c ON p.CategoryID = c.CategoryID";

            if (!string.IsNullOrEmpty(category))
            {
                query += " WHERE c.Name = @Category";
            }

            using SqlCommand command = new SqlCommand(query, connection);

            if (!string.IsNullOrEmpty(category))
            {
                command.Parameters.AddWithValue("@Category", category);
            }

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ProductDTO product = new ProductDTO
                {
                    ProductID = Convert.ToInt32(reader["ProductID"]),
                    Name = reader["Name"].ToString(),
                    Price = Convert.ToDecimal(reader["Price"]),
                    Description = reader["Description"].ToString(),
                    CategoryID = Convert.ToInt32(reader["CategoryID"]),
                };

                products.Add(product);
            }
        }
        catch (SqlException)
            {
                throw new Exception("Something went wrong. Please try again later.");
            }

            return products;
    }

    public ProductDTO GetProductById(int id)
    {
        try
        {
            ProductDTO product = null;
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = "SELECT ProductID, Name, Price, Description, CategoryID FROM Product WHERE ProductID = @id";
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
                    CategoryID = Convert.ToInt32(reader["CategoryID"])
                };
            }
            return product;
        }

        catch (SqlException)
        {
            throw new Exception("Something went wrong. Please try again later.");
        }
    }

    public void AddProduct(ProductDTO product)
    {
        try 
        { 
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = "INSERT INTO Product (Name, Price, Description, CategoryID) VALUES (@Name, @Price, @Description, @CategoryID)";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@Description", product.Description ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@CategoryID", product.CategoryID);

            command.ExecuteNonQuery();
        }

        catch (SqlException)
        {
            throw new Exception("Something went wrong. Please try again later.");
        }
    }

    public void UpdateProduct(ProductDTO product)
    {
        try
        { 
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = "UPDATE Product SET Name = @Name, Price = @Price, Description = @Description, CategoryID = @CategoryID WHERE ProductID = @ProductID";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ProductID", product.ProductID);
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@Description", product.Description ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@CategoryID", product.CategoryID);

            command.ExecuteNonQuery();
        }

        catch (SqlException)
        {
            throw new Exception("Something went wrong. Please try again later.");
        }
    }

    public bool DeleteProduct(int id)
    {
        try
        { 
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = "DELETE FROM Product WHERE ProductID = @ProductID";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ProductID", id);

            int rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0;
        }

        catch (SqlException)
        {
            throw new Exception("Something went wrong. Please try again later.");
        }
    }
}