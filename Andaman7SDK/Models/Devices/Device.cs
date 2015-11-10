using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andaman7SDK.Models.Devices
{
    public class Device
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<DeviceProperty> properties { get; set; }
        public DateTime lastSynchro { get; set; }
        public Boolean active { get; set; }

        public Device()
        {

        }
    }
}
