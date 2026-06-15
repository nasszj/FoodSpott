using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Interface
{
    public interface IUserRepository
    {
        void Register(UserDTO user);
        bool EmailExists(string email);
    }
}
