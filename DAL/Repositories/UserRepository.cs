namespace DAL.Repositories;
using Interfaces;
using Interfaces.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection");
    }

    public bool EmailExists(string email)
    {
        try
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = "SELECT COUNT(*) FROM [User] WHERE Email = @Email";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", email);

            int count = Convert.ToInt32(command.ExecuteScalar());

            return count > 0;
        }
        catch (SqlException)
        {
            throw new Exception("Something went wrong. Please try again later.");
        }
    }

    public void Register(UserDTO user)
    {
        try
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = "INSERT INTO [User] (Email, Password, Role) VALUES (@Email, @Password, @Role)";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@Role", user.Role);

            command.ExecuteNonQuery();
        }
        catch (SqlException)
        {
            throw new Exception("Something went wrong. Please try again later.");
        }
    }
}