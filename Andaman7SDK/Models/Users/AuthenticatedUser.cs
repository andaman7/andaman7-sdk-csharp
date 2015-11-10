using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andaman7SDK.Models.Users
{
    public class AuthenticatedUser : User
    {
        public string mail { get; set; }
        public Boolean privateProfile { get; set; }
        public Boolean localizationAccepted { get; set; }
        public DateTime validationDate { get; set; }
        public string preferredLanguage { get; set; }
        public List<string> roles { get; set; }

        public AuthenticatedUser() : base()
        {

        }
    }
}
