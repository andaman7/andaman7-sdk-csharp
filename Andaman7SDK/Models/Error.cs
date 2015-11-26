using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andaman7SDK.Models
{
    class Error
    {
        public int status { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public string developerMessage { get; set; }
        public Uri moreInfo { get; set; }
        public string support { get; set; }

        public Error()
        {

        }
    }
}
