using Andaman7SDK.Models.Devices;
using Andaman7SDK.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andaman7SDK.Examples
{
    class DevicesExamples
    {
        static void Main(string[] args)
        {
            Config config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(@"../../A7.json"));
            A7Client client = new A7Client(config);

            DeviceService deviceService = client.DeviceService;
            List<Device> devices = deviceService.GetDevices();
            
            foreach(Device device in devices)
            {
                Console.WriteLine(String.Format("Device id : {0}", device.id));
                Console.WriteLine(String.Format("Device name : {0}", device.name));
                Console.WriteLine(String.Format("Last synchronization with server : {0}", device.lastSynchro));
                Console.WriteLine("Device properties :");

                foreach(DeviceProperty property in device.properties)
                    Console.WriteLine(String.Format("\t{0} : {1}", property.key, property.value));

                Console.WriteLine("------------------------------------------");
                Console.Read();
            }
        }
    }
}
