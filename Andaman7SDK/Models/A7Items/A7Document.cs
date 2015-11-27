using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andaman7SDK.Models.A7Items
{
    public class A7Document
    {
        public string id { get; set; }
        public string content { get; set; }

        public A7Document()
        {

        }

        public A7Document(string id, string content)
        {
            this.id = id;
            this.content = content;
        }
    }
}
