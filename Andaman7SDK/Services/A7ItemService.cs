using Andaman7SDK.Models.A7Items;
using Andaman7SDK.Models.Envelopes;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Andaman7SDK.Services
{
    public class A7ItemService : BaseService<A7ItemsEnvelope>
    {
        public A7ItemService(A7Client client) : base(client, "a7-items")
        {

        }

        public void SendA7Items(String userId, A7ItemsEnvelope syncContent)
        {
            RestRequest request = new RestRequest(String.Format("users/{0}/a7-items", userId), Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(syncContent);
            Client.Execute(request);
        }

        public List<A7ItemsResponseEnvelope> GetA7Items(String userId, String deviceId)
        {
            Dictionary<String, String> parameters = new Dictionary<string, string>();
            parameters.Add("_deviceId", deviceId);

            return ExecuteRequest<EnvelopeMultiItems<A7ItemsResponseEnvelope>>(String.Format("users/{0}/a7-items", userId), Method.GET, parameters).data;
        }
    }
}
