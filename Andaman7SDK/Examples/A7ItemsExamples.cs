﻿using Andaman7SDK.Models.A7Items;
using Andaman7SDK.Models.Devices;
using Andaman7SDK.Models.Document;
using Andaman7SDK.Models.Users;
using Andaman7SDK.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Andaman7SDK.Models.Converter;

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
            List<User> foundUsers = userService.Search(mail: "<TARGET USER EMAIL ADDRESS>");

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

            string deviceId = devices[0].id;
            A7ItemService a7ItemService = client.A7ItemService;

            string sourceDomain = "com.example";

            // Create an A7Item for the EHR
            String ehrId = "4b99752e-4606-43e4-83a0-d4f3731d12ce"; // Your internal EHR ID (should be reused in the future to send additional data)
            A7Item ehr = new A7Item(A7ItemType.AmiSet, ehrId, "amiSet.ehr", null, 8, authUser.id, deviceId, null);

            // Create an A7Item for the document
            string b64EncodedFileContent = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("<YOUR FILE CONTENT>"));
            /*
                To find the types of document supported by Andaman7, please visit the follwing URL
                and search for AMIs starting by "ami.document" : http://a7-software.github.io/andaman7-api/guide/medical-data/types.html#amis
            
                The officially supported MIME types are : "text/plain", "text/html", "application/pdf", "image/jpeg", "image/png", "image/gif", "image/bmp", "image/svg+xml"

                The supported document subject matters are available there : http://a7-software.github.io/andaman7-api/guide/medical-data/types.html#sl_subjectMatter
             */
            string documentId = Guid.NewGuid().ToString();
            Document document = new Document(14, "ami.document.prescription", b64EncodedFileContent, "myFile.rtf", "application/rtf", documentId, "li.generalMedicine", "ACME Hospital", "Dr. House", "My document", DateTime.UtcNow);

            // Create an A7Item for the weight
            A7Item weight = new A7Item(A7ItemType.AMI, "ami.weight", "70", 12, authUser.id, deviceId, ehrId);
            A7Item weightUnit = new A7Item(A7ItemType.AMI, "qualifier.unit", "li.kilogram", 8, authUser.id, deviceId, weight.id);

            // Create an A7Item for the namespace entry
            A7Item namespaceEntry = new A7Item(A7ItemType.AMI, "ami.namespaceEntry", String.Format("{0}:{1}", sourceDomain, ehrId), 8,  authUser.id, deviceId, ehrId);

            List<A7Item> a7Items = new List<A7Item>();
            a7Items.Add(ehr);
            a7Items.AddRange(A7ItemService.GetA7ItemsFromDocument(authUser.id, deviceId, ehrId, document));
            a7Items.Add(weight);
            a7Items.Add(namespaceEntry);

            // Create envelope
            A7DateConverter converter = new A7DateConverter();
            A7ItemsEnvelope syncContent = new A7ItemsEnvelope(deviceId, sourceDomain, ehrId, JsonConvert.SerializeObject(a7Items, converter));
            syncContent.document = new DocumentContent(document.fileId, document.content);
            
            // Send the data to A7 server
            a7ItemService.SendA7Items(recipientUser.id, syncContent);
            #endregion

            #region Receiver part
            Credentials receiverCredentials = new Credentials("<RECEIVER EMAIL ADDRES>", "<RECEIVER PASSWORD>");
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
                    DocumentContent documentContent = responseEnvelope.document;
                    Console.Out.WriteLine(String.Format("\t\t{0} : {1}", documentContent.id, documentContent.content));
                }

                // Send an ACK to the server
                receiverClient.A7ItemService.AcknowledgeA7ItemsEnvelope(receiverUser.id, responseEnvelope.id);
            }

            Console.Read();
            #endregion  
        }
    }
}
