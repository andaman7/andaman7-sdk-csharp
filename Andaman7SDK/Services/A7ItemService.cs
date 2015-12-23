using Andaman7SDK.Models.A7Items;
using Andaman7SDK.Models.Document;
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

        public void AcknowledgeA7ItemsEnvelope(String userId, string envelopeId)
        {
            RestRequest request = new RestRequest(String.Format("/users/{0}/a7-items/{1}", userId, envelopeId), Method.PUT);
            Client.Execute(request);
        }

        public static List<A7Item> GetA7ItemsFromDocument(string authUserId, string deviceId, string ehrId, Document document)
        {
            List<A7Item> a7Items = new List<A7Item>();

            // Document (AMI)
            A7Item a7ItemDocument = new A7Item(A7ItemType.AMI, document.type, document.fileId, document.version, authUserId, deviceId, ehrId);
            a7ItemDocument.multiId = document.multiId;
            a7Items.Add(a7ItemDocument);
            
            // Document name (Qualifier)
            a7Items.Add(new A7Item(A7ItemType.Qualifier, "qualifier.filename", document.name, document.version, authUserId, deviceId, a7ItemDocument.id));
            
            // Document MIME type (Qualifier)
            a7Items.Add(new A7Item(A7ItemType.Qualifier, "qualifier.mimetype", document.mimeType, document.version, authUserId, deviceId, a7ItemDocument.id));
            
            // Document creation date (Qualifier)
            a7Items.Add(new A7Item(A7ItemType.Qualifier, "qualifier.date", document.creationDate.ToString(), document.version, authUserId, deviceId, a7ItemDocument.id));

            // Document subject matter (Qualifier)
            a7Items.Add(new A7Item(A7ItemType.Qualifier, "qualifier.subjectMatter", document.subjectMatter, document.version, authUserId, deviceId, a7ItemDocument.id));

            // Document description (Qualifier)
            a7Items.Add(new A7Item(A7ItemType.Qualifier, "qualifier.description", document.description, document.version, authUserId, deviceId, a7ItemDocument.id));

            // Document date (Qualifier)
            a7Items.Add(new A7Item(A7ItemType.Qualifier, "qualifier.date", document.date.ToString(), document.version, authUserId, deviceId, a7ItemDocument.id));


            return a7Items;
        }
    }
}
