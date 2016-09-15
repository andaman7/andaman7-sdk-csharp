using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andaman7SDK.Models.A7Items
{
    public class A7Item
    {
        public string id { get; set; }
        public DateTime creationDate { get; set; }
        public string creatorDeviceId { get; set; }
        public string creatorRegistrarId { get; set; }
        public string type { get; set; }
        public string key { get; set; }
        public string value { get; set; }
        public int version { get; set; }
        public string multiId { get; set; }
        public string parentId { get; set; }

        public A7Item()
        {

        }

        public A7Item(A7ItemType type, String key, String value, int version, String creatorUserId, String creatorDeviceId, String parentId) :
            this(type, System.Guid.NewGuid().ToString(), key, value, version, creatorUserId, creatorDeviceId, parentId)
        {
            
        }

        public A7Item(A7ItemType type, String id, String key, String value, int version, String creatorUserId, String creatorDeviceId, String parentId)
        {
            this.id = id;
            this.creationDate = DateTime.UtcNow;
            this.creatorDeviceId = creatorDeviceId;
            this.creatorRegistrarId = creatorUserId;
            this.key = key;
            this.value = value;
            this.version = version;
            this.type = type.Type;
            this.parentId = parentId;
        }
    }
}
