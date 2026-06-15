using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ServiceLibrary.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public User(int userID, string email, string password, string role)
        {
            UserID = userID;
            Email = email;
            Password = password;
            Role = role;
        }
    }
}
