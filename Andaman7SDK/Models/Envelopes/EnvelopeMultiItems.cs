using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andaman7SDK.Models.Envelopes
{
    public class EnvelopeMultiItems<T> : BaseEnvelope
    {
        public List<T> data { get; set; }

        public EnvelopeMultiItems()
        {

        }
    }
}
