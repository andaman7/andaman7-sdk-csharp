using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andaman7SDK.Models.A7Items
{
    public class A7ItemType
    {
        public String Type { get; set; }

        private A7ItemType(String type)
        {
            Type = type;
        }

        public static A7ItemType AMI { get { return new A7ItemType("AMI"); } }
        public static A7ItemType AmiSet { get { return new A7ItemType("AmiSet"); } }
        public static A7ItemType Qualifier { get { return new A7ItemType("Qualifier"); } }
        public static A7ItemType AmiRef { get { return new A7ItemType("AmiRef"); } }
    }
}
