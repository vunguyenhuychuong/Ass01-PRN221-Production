using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class RoleUser
    {
        public String Email { get; set; }
        public String Password { get; set; }
        public String Role { get; set; }

        public RoleUser(string email, string password, string role)
        {
            Email = email;
            Password = password;
            Role = role;
        }

        public RoleUser()
        {
        }
    }
}
