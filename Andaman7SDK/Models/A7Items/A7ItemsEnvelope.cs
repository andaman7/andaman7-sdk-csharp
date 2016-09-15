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
        public string sourceDomain { get; set; }
        public string ehrId { get; set; }
        public string a7Items { get; set; }
        public DocumentContent document { get; set; }

        public A7ItemsEnvelope()
        {

        }

        public A7ItemsEnvelope(string sourceDeviceId, string sourceDomain, string ehrId, string a7Items) :
            this(sourceDeviceId, sourceDomain, ehrId, a7Items, null)
        {
        }

        public A7ItemsEnvelope(string sourceDeviceId, string sourceDomain, string ehrId, string a7Items, DocumentContent document)
        {
            this.sourceDeviceId = sourceDeviceId;
            this.sourceDomain = sourceDomain;
            this.ehrId = ehrId;
            this.a7Items = a7Items;
            this.document = document;
        }
    }
}
