using System;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

class Program
{
    static async Task Main(string[] args)
    {

        //normesta (2023). Tutorial: Work with Azure Queue Storage queues in .NET. [online] Microsoft.com. Available at: https://learn.microsoft.com/en-us/azure/storage/queues/storage-tutorial-queues?tabs=environment-variable-windows [Accessed 16 Oct. 2023].

        string connectionString = "DefaultEndpointsProtocol=https;AccountName=cldv2storage;AccountKey=yoAaCHpKDy4f1ZD/q7mpwFU4sqSam4I8QkdlHl8HdZG6XDn0JDW5dHDafXOGohn00jAs6ihSDPz2+ASt8nEtsQ==;EndpointSuffix=core.windows.net";
        string queueName = "messagequeue";

        // Create a queue client
        QueueClient queueClient = new QueueClient(connectionString, queueName);

        // Create the queue if it doesn't exist
        await queueClient.CreateAsync();

        // Insert a message into the queue

        //Driedger, K. (2012). How do I encode and decode a base64 string? [online] Stack Overflow. Available at: https://stackoverflow.com/questions/11743160/how-do-i-encode-and-decode-a-base64-string [Accessed 16 Oct. 2023].
        string messageContent = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("11876385:Midrand:2022-10-12:SV874H4"));
        string messageContent1 = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("11807465:Pretoria:2023-08-09:SJ84KL"));
        string messageContent2 = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("SP04D9:2023-04-10:Midrand:1185932"));
        string messageContent3 = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("11858062:Bluehills:2022-06-12:SK785N5"));
        string messageContent4 = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("SQ695V8:2023-07-06:Bluehills:11869036"));




        await queueClient.SendMessageAsync(messageContent);
        await queueClient.SendMessageAsync(messageContent1);
        await queueClient.SendMessageAsync(messageContent2);
        await queueClient.SendMessageAsync(messageContent3);
        await queueClient.SendMessageAsync(messageContent4);
        Console.WriteLine("Message added to the queue.");
    }
}
