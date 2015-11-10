using Andaman7SDK.Models.Envelopes;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andaman7SDK.Services
{
    public abstract class BaseService<T> where T : new()
    {
        private JsonDeserializer Deserializer { get; set; }

        protected A7Client Client { get; set; }
        protected string ResourceName { get; }

        public BaseService(A7Client client, String resourceName)
        {
            this.Client = client;
            this.ResourceName = resourceName;
            this.Deserializer = new JsonDeserializer();
        }

        protected K ExecuteRequest<K>(string resource, Method method, Dictionary<string, string> parameters = null, Object body = null) where K : new()
        {
            if (parameters == null)
                parameters = new Dictionary<string, string>();

            RestRequest request = new RestRequest(resource, method);
            request.RequestFormat = DataFormat.Json;

            foreach (string key in parameters.Keys)
                request.AddParameter(key, parameters[key]);

            if (body != null)
            {
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(body);
            }
            else
            {
                request.AddQueryParameter("_envelope", "true");
            }

            IRestResponse<K> response = Client.Execute<K>(request);
            K envelope = Deserializer.Deserialize<K>(response);

            return envelope;
        }

        protected List<T> GetAll()
        {
            EnvelopeMultiItems<T> envelope = ExecuteRequest<EnvelopeMultiItems<T>>(ResourceName, Method.GET);
            return envelope.data;
        }

        protected T Get(string id)
        {
            EnvelopeSingleItem<T> envelope = ExecuteRequest<EnvelopeSingleItem<T>>(String.Format("{0}/{1}", ResourceName, id), Method.GET);
            return envelope.data;
        }

        protected EnvelopeMultiItems<T> GetPage(int page, int itemsPerPage, Dictionary<string, string> parameters = null)
        {
            if (parameters == null)
                parameters = new Dictionary<string, string>();

            parameters.Add("page", page.ToString());
            parameters.Add("perPage", page.ToString());

            EnvelopeMultiItems<T> envelope = ExecuteRequest<EnvelopeMultiItems<T>>(ResourceName, Method.GET, parameters);
            return envelope;
        }

        protected List<T> Search(Dictionary<string, string> parameters)
        {
            EnvelopeMultiItems<T> envelope = ExecuteRequest<EnvelopeMultiItems<T>>(ResourceName, Method.GET, parameters);
            return envelope.data;
        }

        protected T Create(T item)
        {
            T createdItem = ExecuteRequest<T>(ResourceName, Method.POST);
            return createdItem;
        }

        protected T Update(T item)
        {
            T updatedItem = ExecuteRequest<T>(ResourceName, Method.PUT);
            return updatedItem;
        }

        protected T Delete(T item)
        {
            T deletedItem = ExecuteRequest<T>(ResourceName, Method.DELETE);
            return deletedItem;
        }
    }
}
