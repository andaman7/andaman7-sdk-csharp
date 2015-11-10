using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andaman7SDK.Models.Users
{
    public class User
    {
        public string id { get; set; }
        public string self { get; set; }
        public DateTime creationDate { get; set; }
        public string creatorDeviceId { get; set; }
        public DateTime modificationDate { get; set; }
        public string modifierDeviceId { get; set; }
        public string type { get; set; }
        public Administrative administrative { get; set; }

        public User()
        {

        }
    }
}
