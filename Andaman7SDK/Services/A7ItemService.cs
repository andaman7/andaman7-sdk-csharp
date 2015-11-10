using Andaman7SDK.Models.A7Items;
using Andaman7SDK.Models.Envelopes;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andaman7SDK.Services
{
    public class A7ItemService : BaseService<SyncContent>
    {
        public A7ItemService(A7Client client) : base(client, "a7-items")
        {

        }

        public void SendA7Items(String userId, SyncContent syncContent)
        {
            RestRequest request = new RestRequest(String.Format("users/{0}/a7-items", userId), Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(syncContent);
            Client.Execute(request);
        }

        public List<ResultSyncContent> GetA7Items(String userId, String deviceId)
        {
            Dictionary<String, String> parameters = new Dictionary<string, string>();
            parameters.Add("_deviceId", deviceId);

            return ExecuteRequest<EnvelopeMultiItems<ResultSyncContent>>(String.Format("users/{0}/a7-items", userId), Method.GET, parameters).data;
        }
    }
}
