using Interfaces;
using Interfaces.Interface;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest.FakeRepositories
{
    public class FakeUserRepository : IUserRepository
    {
        private List<UserDTO> users = new List<UserDTO>
        {
            new UserDTO { UserID = 1, Email = "user@gmail.nl", Password = BCrypt.Net.BCrypt.HashPassword("Welkom123"), Role = "User" }
        };

        public void Register(UserDTO user)
        {
            user.UserID = users.Count + 1;
            users.Add(user);
        }

        public bool EmailExists(string email)
        {
            return users.Any(u => u.Email == email);
        }

        public UserDTO GetUserByEmail(string email)
        {
            return users.FirstOrDefault(u => u.Email == email);
        }
    }
}