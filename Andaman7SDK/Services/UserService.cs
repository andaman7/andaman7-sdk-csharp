using Andaman7SDK;
using Andaman7SDK.Models;
using Andaman7SDK.Models.Envelopes;
using Andaman7SDK.Models.Users;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andaman7SDK.Services
{
    public class UserService : BaseService<User>
    {
        public UserService(A7Client client) : base(client, "users")
        {

        }

        public List<User> GetUsers(int page = -1, int perPage = -1)
        {
            return GetPage(page, perPage).data;
        }

        public List<User> Search(String firstName = null, String lastName = null, String country = null, String mail = null)
        {
            Dictionary<String, String> parameters = new Dictionary<string, string>();

            if (firstName != null)
                parameters.Add("administrative.firstName", firstName);

            if (lastName != null)
                parameters.Add("administrative.lastName", lastName);

            if (country != null)
                parameters.Add("administrative.address.country", country);

            if (mail != null)
                parameters.Add("mail", mail);

            return Search(parameters);
        }

        public User SearchByEmail(String mail)
        {
            Dictionary<String, String> parameters = new Dictionary<string, string>();
            parameters.Add("mail", mail);
            List<User> users = Search(parameters);

            if (users.Count == 0)
                return null;
            else
                return users[0];
        }

        public User GetUser(String id)
        {
            return Get(id);
        }

        public AuthenticatedUser GetAuthenticatedUser()
        {
            EnvelopeSingleItem<AuthenticatedUser> envelope =
                ExecuteRequest<EnvelopeSingleItem<AuthenticatedUser>>(
                    String.Format("{0}/me", ResourceName),
                    Method.GET);

            return envelope.data;
        }
    }
}
