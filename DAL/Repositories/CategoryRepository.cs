using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Interfaces;
using Interfaces.Interface;

namespace DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string _connectionString;

        public CategoryRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public List<CategoryDTO> GetAllCategories()
        {
            List<CategoryDTO> categories = new List<CategoryDTO>();

            try
            {
                using SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();

                string query = "SELECT CategoryID, Name FROM Category";

                using SqlCommand command = new SqlCommand(query, connection);
                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CategoryDTO category = new CategoryDTO
                    {
                        CategoryID = Convert.ToInt32(reader["CategoryID"]),
                        Name = reader["Name"].ToString()
                    };

                    categories.Add(category);
                }

                return categories;
            }

            catch (SqlException)
            {
                throw new Exception("Something went wrong. Please try again later.");
            }
        }
    }
}