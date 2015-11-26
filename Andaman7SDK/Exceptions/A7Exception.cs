using Andaman7SDK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andaman7SDK.Exceptions
{
    class A7Exception : Exception
    {
        public Error error { get; set; }

        public A7Exception(Error error) : base(error.message)
        {
            this.error = error;
        }
    }
}
