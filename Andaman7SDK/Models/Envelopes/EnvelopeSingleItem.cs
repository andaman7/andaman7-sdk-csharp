using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andaman7SDK.Models.Envelopes
{
    public class EnvelopeSingleItem<T> : BaseEnvelope
    {
        public T data { get; set; }

        public EnvelopeSingleItem()
        {

        }
    }
}
