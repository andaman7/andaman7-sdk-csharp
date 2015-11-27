using Andaman7SDK.Models.A7Items;
using Andaman7SDK.Models.Devices;
using Andaman7SDK.Models.Users;
using Andaman7SDK.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Andaman7SDK.Examples
{
    class A7ItemsExamples
    {
        static void Main(string[] args)
        {
            #region Sender part
            Config config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(@"../../config.json"));
            A7Client client = new A7Client(config);

            // Search target user
            UserService userService = client.UserService;
            //List<User> foundUsers = userService.Search(mail: "<TARGET USER EMAIL ADDRESS>");
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
            A7Item ehr = new A7Item(A7ItemType.AmiSet, ehrId, "amiSet.ehr", null, authUser.id, deviceId, null);
            ehr.version = 8;

            // Create an A7Item for the document
            String fileId = System.Guid.NewGuid().ToString(); // The file ID. Will be used in A7Item value and as key in the file map
            String documentId = "478e1332-5383-43c0-878f-fe1e8a4e6d01"; // The ID of the document. It should not change if the document can be modified and sent again
            A7Item a7ItemDocument = new A7Item(A7ItemType.AMI, "ami.document.bloodAnalysis", fileId, authUser.id, deviceId, ehrId);
            a7ItemDocument.parentId = ehrId;
            a7ItemDocument.uuidMulti = documentId;
            a7ItemDocument.version = 8;

            // Create an A7Item for the weight
            A7Item height = new A7Item(A7ItemType.AMI, "ami.height", "185", authUser.id, deviceId, ehrId);
            height.version = 8;

            // Create an A7Item for the namespace entry
            A7Item namespaceEntry = new A7Item(A7ItemType.AMI, "ami.namespaceEntry", "be.ac.ulg.chu", authUser.id, deviceId, ehrId);
            namespaceEntry.version = 8;

            // Create an A7Item for the namespace value
            A7Item namespaceValue = new A7Item(A7ItemType.Qualifier, "qualifier.namespaceValue", ehrId, authUser.id, deviceId, namespaceEntry.id);
            namespaceValue.version = 8;

            List<A7Item> a7Items = new List<A7Item>();
            a7Items.Add(ehr);
            a7Items.Add(a7ItemDocument);
            a7Items.Add(height);
            a7Items.Add(namespaceEntry);
            a7Items.Add(namespaceValue);

            // Create envelope
            A7ItemsEnvelope syncContent = new A7ItemsEnvelope();
            syncContent.sourceDeviceId = deviceId;
            syncContent.a7Items = JsonConvert.SerializeObject(a7Items);
            syncContent.document = new A7Document(fileId, "<BASE64 ENCODED FILE CONTENT>");
            
            // Send the data to A7 server
            a7ItemService.SendA7Items(recipientUser.id, syncContent);
            #endregion

            #region Receiver part
            //Credentials receiverCredentials = new Credentials("<RECEIVER EMAIL ADDRES>", "<RECEIVER PASSWORD>");
            Credentials receiverCredentials = new Credentials("a@a.com", "aaaaaa");
            Config receiverConfig = new Config(config.BaseUrl, config.ApiKey, receiverCredentials);
            A7Client receiverClient = new A7Client(receiverConfig);

            AuthenticatedUser receiverUser = receiverClient.UserService.GetAuthenticatedUser();
            List<Device> receiverDevices = receiverClient.DeviceService.GetDevices();
            List<A7ItemsResponseEnvelope> responseEnvelopes = receiverClient.A7ItemService.GetA7Items(receiverUser.id, receiverDevices[0].id);
                       
            foreach(A7ItemsResponseEnvelope responseEnvelope in responseEnvelopes)
            {
                // Deserialize and display the received A7 items
                List<A7Item> receivedA7Items = JsonConvert.DeserializeObject<List<A7Item>>(responseEnvelope.a7Items);
                Console.Out.WriteLine(String.Format(" {0} A7Items received from {1}.", receivedA7Items.Count, responseEnvelope.sourceDeviceId));

                foreach (A7Item a7Item in receivedA7Items)
                {
                    Console.Out.WriteLine("\t{0} : {1}", a7Item.key, a7Item.value == null ? "N/A" : a7Item.value);
                }

                if (responseEnvelope.document != null)
                {
                    A7Document document = responseEnvelope.document;
                    Console.Out.WriteLine(String.Format("\t\t{0} : {1}", document.id, document.content));
                }

                // Send an ACK to the server
                receiverClient.A7ItemService.AcknowledgeA7ItemsEnvelope(receiverUser.id, responseEnvelope.id);
            }

            Console.Read();
            #endregion
        }
    }
}
