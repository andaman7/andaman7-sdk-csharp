using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andaman7SDK
{
    public class Credentials
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public Credentials()
        {

        }

        public Credentials(String email, String password)
        {
            Email = email;
            Password = password;
        }
    }
}
