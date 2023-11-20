
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;

namespace ST10029788_AariyaSingh_CLDV6212_POE_Part2_QUESTION_B
{
    public class VaccinationData
    {
        public string Id { get; set; }
        public string VaccinationCenter { get; set; }
        public string VaccinationDate { get; set; }
        public string VaccineSerialNumber { get; set; }
    }

    public static class VaccinationStatusFunction
    {
        [FunctionName("ProcessVaccinationQueue")]
        public static async Task RunAsync(
            [QueueTrigger("messagequeue", Connection = "queuecon")] string message,
            ILogger log)
        {
            log.LogInformation($"Received message: {message}");


            // Butt, M. (2021). Using Azure Blob Storage In C#. [online] C-sharpcorner.com. Available at: https://www.c-sharpcorner.com/article/using-azure-blob-storage-in-c-sharp/ [Accessed 20 Nov. 2023].
            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=cldv2storage;AccountKey=yoAaCHpKDy4f1ZD/q7mpwFU4sqSam4I8QkdlHl8HdZG6XDn0JDW5dHDafXOGohn00jAs6ihSDPz2+ASt8nEtsQ==;EndpointSuffix=core.windows.net";
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Split the message into its components
            string[] messageParts = message.Split(':');

            if (messageParts.Length == 4)
            {
                var vaccinationData = new VaccinationData
                {
                    Id = messageParts[3],
                    VaccinationCenter = messageParts[2],
                    VaccinationDate = messageParts[1],
                    VaccineSerialNumber = messageParts[0]
                };

                // Serialize the data to JSON
                string jsonData = JsonConvert.SerializeObject(vaccinationData);

                // Save JSON data to Blob Storage
                await SaveDataToBlobStorage(blobClient, jsonData, vaccinationData.Id);
            }
            else
            {
                log.LogError("Invalid message format");
            }

            log.LogInformation("Stored data in Blob Storage.");
        }

        private static async Task SaveDataToBlobStorage(CloudBlobClient blobClient, string jsonData, string id)
        {
            CloudBlobContainer container = blobClient.GetContainerReference("part3");
            await container.CreateIfNotExistsAsync();

            CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{id}.json");

            using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonData)))
            {
                // Await the asynchronous upload operation
                await blockBlob.UploadFromStreamAsync(stream);
            }
        }

    }
}

