using RestSharp.Deserializers;
using RestSharp.Serializers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andaman7SDK.Models.A7Items
{
    public class A7ItemsEnvelope
    {
        public string sourceDeviceId { get; set; }
        public string a7Items { get; set; }
        public A7Document document { get; set; }

        public A7ItemsEnvelope()
        {
            
        }
    }
}
