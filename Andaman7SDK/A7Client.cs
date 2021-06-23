using Andaman7SDK.Exceptions;
using Andaman7SDK.Models;
using Andaman7SDK.Services;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Andaman7SDK
{
    public class A7Client
    {
        private Config Config;
        private RestClient RestClient;
        private JsonDeserializer Deserializer;

        public UserService UserService { get; }
        public DeviceService DeviceService { get; }
        public A7ItemService A7ItemService { get; }

        public A7Client(Config config)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            this.Config = config;
            this.RestClient = new RestClient(config.BaseUrl);
            this.UserService = new UserService(this);
            this.DeviceService = new DeviceService(this);
            this.A7ItemService = new A7ItemService(this);
            this.Deserializer = new JsonDeserializer();

            String email = Config.Credentials.Email;
            String password = config.Credentials.Password;
            String apiKey = config.ApiKey;
            
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var hashedPasswordBytes = SHA256.Create().ComputeHash(passwordBytes);
            string hashString = string.Empty;

            foreach (byte x in hashedPasswordBytes)
            {
                hashString += String.Format("{0:x2}", x);
            }

            RestClient.Authenticator = new HttpBasicAuthenticator(email, hashString);
            RestClient.AddDefaultHeader("api-key", apiKey);
        }

        public T Execute<T>(IRestRequest request) where T : new()
        {
            IRestResponse<T> response = RestClient.Execute<T>(request);
            HandleError(response);

            return Deserializer.Deserialize<T>(response);
        }

        public void Execute(IRestRequest request)
        {
            IRestResponse response = RestClient.Execute(request);
            HandleError(response);
        }

        private void HandleError(IRestResponse response)
        {
            if (response.StatusCode != System.Net.HttpStatusCode.OK &&
                response.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                Error error = Deserializer.Deserialize<Error>(response);
                throw new A7Exception(error);
            }
        }
    }
}
