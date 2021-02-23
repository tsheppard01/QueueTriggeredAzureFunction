using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;

namespace FunctionsProj
{
    public class QueueTriggeredFunctionRunner
    {
        private readonly BlobContainerClient _blobContainerClient;
        private readonly ILogger _logger;

        public QueueTriggeredFunctionRunner(BlobContainerClient blobContainerClient, ILogger logger)
        {
            _blobContainerClient = blobContainerClient;
            _logger = logger;
        }

        public async Task ProcessQueueItem(string queueItem)
        {

            var blobName = $"{Guid.NewGuid()}.txt";

            var outputStream = new MemoryStream(Encoding.UTF8.GetBytes(queueItem));

            var result = await _blobContainerClient
                .UploadBlobAsync(blobName, outputStream)
                .ConfigureAwait(false);

            outputStream.Dispose();

            _logger.LogInformation(
                $"Uploaded blob {blobName} to azure storage container {_blobContainerClient.Name}, " +
                $"version Id: {result.Value.VersionId}");
        }
    }
}
