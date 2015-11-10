using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andaman7SDK.Models.Users
{
    public class Administrative
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string picture { get; set; }
        public Address address { get; set; }
        public string phoneProfessional { get; set; }
        public string mailProfessional { get; set; }
        public string doctorFunctions { get; set; }

        public Administrative()
        {

        }
    }
}
