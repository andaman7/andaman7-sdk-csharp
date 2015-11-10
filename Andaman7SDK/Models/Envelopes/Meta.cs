using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andaman7SDK.Models.Envelopes
{
    [Serializable]
    public class Meta
    {
        public int page { get; set; }
        public int perPage { get; set; }
        public int totalPages { get; set; }
        public int count { get; set; }
        public int totalItems { get; set; }
        public bool hasMore { get; set; }

        public Meta()
        {

        }
    }
}
