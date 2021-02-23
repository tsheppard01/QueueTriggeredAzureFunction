using System;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionsProj
{
    public static class QueueTriggeredFunction
    {
        [FunctionName("QueueTriggeredFunction")]
        public static async Task Run([QueueTrigger("%SourceQueueName%", Connection = "SourceQueueConnection")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");

            var blobContainerClientConnection = Environment.GetEnvironmentVariable("DestinationContainerConnection");
            var blobContainerName = Environment.GetEnvironmentVariable("DestinationContainerName");

            var blobContainerClient = new BlobContainerClient(blobContainerClientConnection, blobContainerName);

            var runner = new QueueTriggeredFunctionRunner(blobContainerClient, log);

            await runner.ProcessQueueItem(myQueueItem);
        }
    }
}
