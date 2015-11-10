using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andaman7SDK.Models.Users
{
    public class Address
    {
        public string street { get; set; }
        public string number { get; set; }
        public string box { get; set; }
        public string zip { get; set; }
        public string town { get; set; }
        public string country { get; set; }

        public Address()
        {

        }
    }
}
