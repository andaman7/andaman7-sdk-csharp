using Andaman7SDK.Models.Users;
using Andaman7SDK.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andaman7SDK.Examples
{
    class UsersExamples
    {
        static void Main(string[] args)
        {
            Config config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(@"../../A7.json"));
            A7Client client = new A7Client(config);
            UserService userService = client.UserService;

            // Search users by first name and last name
            List<User> foundUsers = userService.Search(firstName: "John", lastName: "Doe");

            foreach(User foundUser in foundUsers)
            {
                DisplayUser(foundUser);
                Console.WriteLine("-----------------------------------------");
            }

            // Search user by email (exact match)
            User user = userService.SearchByEmail("a@a.com");
            DisplayUser(user);
            Console.WriteLine("-----------------------------------------");

            // Get information about the authenticated user
            AuthenticatedUser authUser = userService.GetAuthenticatedUser();
            DisplayAuthenticatedUser(authUser);
            Console.WriteLine("-----------------------------------------");

            Console.Read();
        }

        public static void DisplayUser(User user)
        {
            Console.WriteLine(String.Format("Id : {0}", user.id));
            Console.WriteLine(String.Format("First name : {0}", user.administrative.firstName));
            Console.WriteLine(String.Format("Last name : {0}", user.administrative.lastName));
            Console.WriteLine(String.Format("Street : {0}", user.administrative.address.street));
            Console.WriteLine(String.Format("Number : {0}", user.administrative.address.number));
            Console.WriteLine(String.Format("Box : {0}", user.administrative.address.box));
            Console.WriteLine(String.Format("Zip code : {0}", user.administrative.address.zip));
            Console.WriteLine(String.Format("Town : {0}", user.administrative.address.town));
            Console.WriteLine(String.Format("Country : {0}", user.administrative.address.country));
        }

        public static void DisplayAuthenticatedUser(AuthenticatedUser authUser)
        {
            DisplayUser(authUser);

            Console.WriteLine(String.Format("Mail : {0}", authUser.mail));
            Console.WriteLine(String.Format("Private profile ? : {0}", authUser.privateProfile));
            Console.WriteLine(String.Format("Localization accepted ? : {0}", authUser.localizationAccepted));
            Console.WriteLine(String.Format("Validation date : {0}", authUser.validationDate));
            Console.WriteLine(String.Format("Preferred language : {0}", authUser.preferredLanguage));

            Console.Write("Roles : ");
            foreach(String role in authUser.roles)
            {
                Console.Write(String.Format("{0}, ", role));
            }
            Console.WriteLine();
        }
    }
}
