using RestSharp.Deserializers;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andaman7SDK.Models.A7Items
{
    public class SyncContent
    {
        public string sourceDeviceId { get; set; }
        public string a7Items { get; set; }
        public string fileMap { get; set; }

        public SyncContent()
        {
            
        }
    }
}
