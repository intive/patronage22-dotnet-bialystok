using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using Patronage.Contracts.Interfaces;

namespace Patronage.DataAccess.Services
{
    public class BlobService : IBlobService
    {
        private readonly ILogger<BlobService> _logger;
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(ILogger<BlobService> logger, BlobServiceClient blobServiceClient)
        {
            _logger = logger;
            _blobServiceClient = blobServiceClient;
        }

        public async Task GetBlobAsync(string blobContainerName, string localDirectory)
        {
            try
            {
                _logger.LogDebug($"Downloading {blobContainerName} from Azure Blob Storage");
                var containerClient = _blobServiceClient.GetBlobContainerClient("herokulogs");
                if (Directory.Exists(localDirectory))
                {
                    Directory.CreateDirectory($"{localDirectory}");
                }
                var path = $@"./{localDirectory}";
                List<Task> tasks = new List<Task>();
                var blobs = containerClient.GetBlobs();
                foreach (var blob in blobs)
                {
                    var blobpath = Path.Combine(path, blob.Name);

                    tasks.Add(Task.Run(() => containerClient.GetBlobClient(blob.Name).DownloadToAsync(blobpath)));
                }
                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error trying to download {blobContainerName} from Azure Blob Storage", ex);
            }
        }

        public async Task UploadBlobsAsync(string containerName, string directory)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();
            string[] fileEntries = Directory.GetFiles($@"./{directory}");
            List<Task> tasks = new List<Task>();
            foreach (string fileEntry in fileEntries)
            {
                _logger.LogInformation("uploading " + $@"{fileEntry}");
                tasks.Add(Task.Run(() => containerClient.GetBlobClient(fileEntry).UploadAsync(fileEntry)));
            };
            await Task.WhenAll(tasks);
            _logger.LogInformation("Upload completed");
        }
    }
}