using System;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using System.Threading.Tasks;

namespace ReceiveEventHubClient
{
    class Program
    {
        private const string EventHubConnectionString = "Endpoint=sb://azehub.servicebus.windows.net/;SharedAccessKeyName=mysharedaccesspolicy;SharedAccessKey=YJkHm7rg6/lxryGGk7ynYb5s5LYT3vwpfS1DY5ZsaoU=;EntityPath=myeventhub";
        private const string EventHubName = "myeventhub";
        private const string StorageContainerName = "azeventhubcontainer";
        private const string StorageAccountName = "azeventhub";
        private const string StorageAccountKey = "DFfhsd4CJUzc1zWgLJ053q6Cx8XHh9RZJ8N+6G4TI3rb1EIhkv6Sjbh13avNargw1xgg4ORSjZdcpmS0RvixQQ==";

        private static readonly string StorageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", StorageAccountName, StorageAccountKey);
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Receiver ....");
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            Console.WriteLine("Registering EventProcessor...");

            var eventProcessorHost = new EventProcessorHost(
                EventHubName,
                PartitionReceiver.DefaultConsumerGroupName,
                EventHubConnectionString,
                StorageConnectionString,
                StorageContainerName);

            // Registers the Event Processor Host and starts receiving messages
            await eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>();

            Console.WriteLine("Receiving. Press ENTER to stop worker.");
            Console.ReadLine();

            // Disposes of the Event Processor Host
            await eventProcessorHost.UnregisterEventProcessorAsync();
        }
    }
}
