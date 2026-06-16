using Interfaces;
using Interfaces.Interface;
using ServiceLibrary.Models;
using ServiceLibrary.Models.Mappers;
using BCrypt.Net;

namespace ServiceLibrary.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Register(string email, string password, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            if (password.Length < 8)
            {
                return false;
            }

            if (password != confirmPassword)
            {
                return false;
            }

            if (_userRepository.EmailExists(email))
            {
                return false;
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            User user = new User(
                0,
                email,
                hashedPassword,
                "User"
            );

            UserDTO dto = UserMapper.UserDTOFromModel(user);

            _userRepository.Register(dto);

            return true;
        }
    }
}