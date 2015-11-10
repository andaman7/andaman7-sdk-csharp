using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andaman7SDK.Models.A7Items
{
    class A7Item
    {
        public String id { get; set; }
        public DateTime creationDate { get; set; }
        public String creatorDeviceId { get; set; }
        public String creatorUserId { get; set; }
        public String type { get; set; }
        public String key { get; set; }
        public String value { get; set; }
        public int version { get; set; }
        public String uuidMulti { get; set; }
        public String parentId { get; set; }

        public A7Item()
        {

        }

        public A7Item(A7ItemType type, String key, String value, String creatorUserId, String creatorDeviceId) :
            this(type, System.Guid.NewGuid().ToString(), key, value, creatorUserId, creatorDeviceId)
        {
            
        }

        public A7Item(A7ItemType type, String id, String key, String value, String creatorUserId, String creatorDeviceId)
        {
            this.id = id;
            this.creationDate = new DateTime();
            this.creatorDeviceId = creatorDeviceId;
            this.creatorUserId = creatorUserId;
            this.key = key;
            this.value = value;
            this.type = type.Type;
        }
    }
}
