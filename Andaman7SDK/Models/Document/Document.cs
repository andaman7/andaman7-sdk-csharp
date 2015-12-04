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

        // File related properties
        public string fileId { get; set; }
        public string content { get; set; }

        // Qualifiers
        public string name { get; set; }
        public string mimeType { get; set; }
        public DateTime creationDate { get; set; }
        public string subjectMatter { get; set; }

        public Document(int version, string type, string content, string name, string mimeType) :
            this(Guid.NewGuid().ToString(), version, type, Guid.NewGuid().ToString(), content, name, mimeType, new DateTime(), null)
        {
        }

        public Document(int version, string type, string content, string name, string mimeType, string subjectMatter) :
            this(Guid.NewGuid().ToString(), version, type, Guid.NewGuid().ToString(), content, name, mimeType, new DateTime(), subjectMatter)
        {
        }

        public Document(int version, string type, string content, string name, string mimeType, DateTime creationDate, string subjectMatter) :
            this(Guid.NewGuid().ToString(), version, type, Guid.NewGuid().ToString(), content, name, mimeType, creationDate, subjectMatter)
        {
        }

        public Document(string id, int version, string type, string fileId, string content, string name, string mimeType, DateTime creationDate, string subjectMatter)
        {
            this.id = id;
            this.version = version;
            this.type = type;

            this.fileId = fileId;
            this.content = content;

            this.name = name;
            this.mimeType = mimeType;
            this.creationDate = creationDate;
            this.subjectMatter = subjectMatter;
        }
    }
}
