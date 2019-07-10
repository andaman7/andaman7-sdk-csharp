using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andaman7SDK.Models.Document
{
    public class Document
    {
        // AMI related properties
        public string id { get; set; }
        public int version { get; set; }
        public string type { get; set; }
        public string multiId { get; set; }

        // File related properties
        public string fileId { get; set; }
        public string content { get; set; }

        // Qualifiers
        public string name { get; set; }
        public string mimeType { get; set; }
        public DateTime creationDate { get; set; }
        public string subjectMatter { get; set; }
        public string careFacility { get; set; }
        public string careProvider { get; set; }
        public string careFacilityId { get; set; }
        public string careProviderId { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }

        public Document(int version, string type, string content, string name, string mimeType, string multiId, string subjectMatter, string careFacilityId, string careProviderId, string description, DateTime date) :
            this(Guid.NewGuid().ToString(), version, type, Guid.NewGuid().ToString(), content, name, mimeType, DateTime.UtcNow, multiId, subjectMatter, null, null, careFacilityId, careProviderId, description, date)
        {
        }

        public Document(string id, int version, string type, string fileId, string content, string name, string mimeType, DateTime creationDate, string multiId, string subjectMatter, string careFacility, string careProvider, string careFacilityId, string careProviderId, string description, DateTime date)
        {
            this.id = id;
            this.version = version;
            this.type = type;
            this.multiId = multiId;

            this.fileId = fileId;
            this.content = content;

            this.name = name;
            this.mimeType = mimeType;
            this.creationDate = creationDate;
            this.subjectMatter = subjectMatter;
            this.careFacility = careFacility;
            this.careProvider = careProvider;
            this.careFacilityId = careFacilityId;
            this.careProviderId = careProviderId;
            this.description = description;
            this.date = date;
        }
    }
}
