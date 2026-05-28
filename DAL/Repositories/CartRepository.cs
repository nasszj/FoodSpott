namespace DAL.Repositories;

using Interfaces;
using Interfaces.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

public class CartRepository : ICartRepository
{
    private readonly string _connectionString;

    public CartRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection");
    }

    public CartDTO GetCart()
    {
        try
        {
            CartDTO cart = null;

            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = "SELECT TOP 1 CartID, UserID, TotalPrice FROM Cart";

            using SqlCommand command = new SqlCommand(query, connection);
            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                cart = new CartDTO
                {
                    CartID = Convert.ToInt32(reader["CartID"]),
                    UserID = reader["UserID"] == DBNull.Value ? null : Convert.ToInt32(reader["UserID"]),
                    TotalPrice = Convert.ToDecimal(reader["TotalPrice"])
                };
            }

            if (cart != null)
            {
                cart.Products = GetCartProducts(cart.CartID);
            }

            return cart;
        }

        catch (SqlException)
        {
            throw new Exception("Something went wrong. Please try again later.");
        }
    }

    private List<CartProductDTO> GetCartProducts(int cartID)
    {
        try
        {
            List<CartProductDTO> products = new List<CartProductDTO>();

            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"
                SELECT cp.CartProductID, cp.CartID, cp.ProductID, p.Name, p.Price, cp.Quantity, cp.Subtotal
                FROM CartProduct cp
                INNER JOIN Product p ON cp.ProductID = p.ProductID
                WHERE cp.CartID = @CartID";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CartID", cartID);

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                CartProductDTO product = new CartProductDTO
                {
                    CartProductID = Convert.ToInt32(reader["CartProductID"]),
                    CartID = Convert.ToInt32(reader["CartID"]),
                    ProductID = Convert.ToInt32(reader["ProductID"]),
                    ProductName = reader["Name"].ToString(),
                    Price = Convert.ToDecimal(reader["Price"]),
                    Quantity = Convert.ToInt32(reader["Quantity"]),
                    Subtotal = Convert.ToDecimal(reader["Subtotal"])
                };

                products.Add(product);
            }

            return products;
        }

        catch (SqlException)
        {
            throw new Exception("Something went wrong. Please try again later.");
        }
    }

    public void AddProduct(int productID)
    {
        int cartID = GetOrCreateCartID();

        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        string query = @"
            INSERT INTO CartProduct (CartID, ProductID, Quantity, Subtotal)
            SELECT @CartID, ProductID, 1, Price
            FROM Product
            WHERE ProductID = @ProductID";

        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@CartID", cartID);
        command.Parameters.AddWithValue("@ProductID", productID);

        command.ExecuteNonQuery();

        UpdateTotalPrice(cartID);
    }

    public void UpdateQuantity(int cartProductID, int quantity)
    {
        try
        {
            int cartID = 0;

            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string getCartQuery = "SELECT CartID FROM CartProduct WHERE CartProductID = @CartProductID";

            using SqlCommand getCartCommand = new SqlCommand(getCartQuery, connection);
            getCartCommand.Parameters.AddWithValue("@CartProductID", cartProductID);

            object result = getCartCommand.ExecuteScalar();

            if (result != null)
            {
                cartID = Convert.ToInt32(result);
            }

            string query = @"
            UPDATE CartProduct
            SET Quantity = @Quantity,
                Subtotal = @Quantity * (SELECT Price FROM Product WHERE ProductID = CartProduct.ProductID)
            WHERE CartProductID = @CartProductID";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CartProductID", cartProductID);
            command.Parameters.AddWithValue("@Quantity", quantity);

            command.ExecuteNonQuery();

            UpdateTotalPrice(cartID);
        }

        catch (SqlException)
        {
            throw new Exception("Something went wrong. Please try again later.");
        }
    }

    public bool DeleteProduct(int cartProductID)
    {
        try
        {
            int cartID = 0;

            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string selectQuery = "SELECT CartID FROM CartProduct WHERE CartProductID = @CartProductID";

            using SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
            selectCommand.Parameters.AddWithValue("@CartProductID", cartProductID);

            object result = selectCommand.ExecuteScalar();

            if (result == null)
            {
                return false;
            }

            cartID = Convert.ToInt32(result);

            string deleteQuery = "DELETE FROM CartProduct WHERE CartProductID = @CartProductID";

            using SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
            deleteCommand.Parameters.AddWithValue("@CartProductID", cartProductID);

            int rowsAffected = deleteCommand.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                UpdateTotalPrice(cartID);
                return true;
            }

            return false;
        }

        catch (SqlException)
        {
            throw new Exception("Something went wrong. Please try again later.");
        }
    }

    private int GetOrCreateCartID()
    {
        try
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string selectQuery = "SELECT TOP 1 CartID FROM Cart";

            using SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
            object result = selectCommand.ExecuteScalar();

            if (result != null)
            {
                return Convert.ToInt32(result);
            }

            string insertQuery = "INSERT INTO Cart (TotalPrice) OUTPUT INSERTED.CartID VALUES (0)";

            using SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
            return Convert.ToInt32(insertCommand.ExecuteScalar());
        }

        catch (SqlException)
        {
            throw new Exception("Something went wrong. Please try again later.");
        }
    }

    private void UpdateTotalPrice(int cartID)
    {
        try
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"
                UPDATE Cart
                SET TotalPrice = (
                    SELECT ISNULL(SUM(Subtotal), 0)
                    FROM CartProduct
                    WHERE CartID = @CartID
                )
                WHERE CartID = @CartID";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CartID", cartID);

            command.ExecuteNonQuery();
        }

        catch (SqlException)
        {
            throw new Exception("Something went wrong. Please try again later.");
        }
    }
}