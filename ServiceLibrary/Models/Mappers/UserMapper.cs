using Interfaces;

namespace ServiceLibrary.Models.Mappers
{
    public class UserMapper
    {
        static public User UserModelFromDto(UserDTO dto)
        {
            return new User(
                dto.UserID,
                dto.Email,
                dto.Password,
                dto.Role
            );
        }

        static public UserDTO UserDTOFromModel(User user)
        {
            return new UserDTO
            {
                UserID = user.UserID,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role
            };
        }
    }
}