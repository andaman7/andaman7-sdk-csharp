using Andaman7SDK.Models.Envelopes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andaman7SDK.Models.Envelopes
{
    [Serializable]
    public abstract class BaseEnvelope
    {
        public BaseEnvelope()
        {

        }

        public Meta meta { get; set; }
        public List<object> links { get; set; }
    }
}
