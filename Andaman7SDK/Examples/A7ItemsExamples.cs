using Andaman7SDK.Models.A7Items;
using Andaman7SDK.Models.Devices;
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
    class A7ItemsExamples
    {
        static void Main(string[] args)
        {
            Config config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(@"../../A7.json"));
            A7Client client = new A7Client(config);

            // Search target user
            UserService userService = client.UserService;
            List<User> foundUsers = userService.Search(mail: "a@a.com");

            if (foundUsers.Count != 1)
            {
                System.Console.WriteLine("The target user has not been found.");
                System.Environment.Exit(1);
            }

            User recipientUser = foundUsers[0];

            // Get authenticated user information and its devices
            AuthenticatedUser authUser = userService.GetAuthenticatedUser();
            DeviceService deviceService = client.DeviceService;
            List<Device> devices = deviceService.GetDevices();

            if (devices.Count == 0)
            {
                System.Console.WriteLine("You must have at least one device to send medical records.");
                System.Environment.Exit(1);
            }

            String deviceId = devices[0].id;
            A7ItemService a7ItemService = client.A7ItemService;

            // Create an A7Item for the EHR
            String ehrId = System.Guid.NewGuid().ToString(); // Your custom EHR ID (should not change in the future)
            A7Item ehr = new A7Item(A7ItemType.AmiSet, "amiSet.ehr", null, authUser.id, deviceId);
            ehr.version = 8;

            // Create an A7Item for the document
            String fileId = System.Guid.NewGuid().ToString(); // The file ID. Will be used in A7Item value and as key in the file map
            String documentId = "478e1332-5383-43c0-878f-fe1e8a4e6d01"; // The ID of the document. It should not change if the document can be modified and sent again
            A7Item document = new A7Item(A7ItemType.AMI, "ami.document.bloodAnalysis", fileId, authUser.id, deviceId);
            document.parentId = ehrId;
            document.uuidMulti = documentId;
            document.version = 8;

            // Create an A7Item for the weight
            A7Item height = new A7Item(A7ItemType.AMI, "ami.height", "185", authUser.id, deviceId);
            height.version = 8;

            // Create an A7Item for the namespace entry
            A7Item namespaceEntry = new A7Item(A7ItemType.AMI, "ami.namespaceEntry", "<YOUR DOMAIN NAME>", authUser.id, deviceId);
            namespaceEntry.version = 8;

            // Create an A7Item for the namespace value
            A7Item namespaceValue = new A7Item(A7ItemType.AMI, "ami.namespaceValue", ehrId, authUser.id, deviceId);
            namespaceValue.version = 8;

            List<A7Item> a7Items = new List<A7Item>();
            a7Items.Add(ehr);
            a7Items.Add(document);
            a7Items.Add(height);
            a7Items.Add(namespaceEntry);
            a7Items.Add(namespaceValue);

            // Create envelope
            SyncContent syncContent = new SyncContent();
            syncContent.sourceDeviceId = deviceId;
            syncContent.a7Items = JsonConvert.SerializeObject(a7Items);

            // Create and serialize the file map
            Dictionary<string, string> fileMap = new Dictionary<string, string>();
            fileMap.Add(fileId, "<BASE64 ENCODED FILE CONTENT>");
            syncContent.fileMap = JsonConvert.SerializeObject(fileMap);

            // Send the data to A7 server
            a7ItemService.SendA7Items(recipientUser.id, syncContent);

            Console.Read();
        }
    }
}
