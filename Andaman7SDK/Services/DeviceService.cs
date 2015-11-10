using Andaman7SDK.Models;
using Andaman7SDK.Models.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andaman7SDK.Services
{
    public class DeviceService : BaseService<Device>
    {
        public DeviceService(A7Client client) : base(client, "users/me/devices")
        {

        }

        public List<Device> GetDevices()
        {
            return GetAll();
        }

        public Device GetDevice(String id)
        {
            return Get(id);
        }

        public Device CreateDevice(Device newDevice)
        {
            return Create(newDevice);
        }

        public Device UpdateDevice(Device updatedDevice)
        {
            return Update(updatedDevice);
        }
    }
}
