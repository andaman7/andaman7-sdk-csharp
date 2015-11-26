using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andaman7SDK
{
    public class Config
    {
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
        public Credentials Credentials { get; set; }

        public Config(string baseUrl, string apiKey, Credentials credentials)
        {
            this.BaseUrl = baseUrl;
            this.ApiKey = apiKey;
            this.Credentials = credentials;
        }
    }

}
