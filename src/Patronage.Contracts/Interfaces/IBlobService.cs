namespace Patronage.Contracts.Interfaces
{
    public interface IBlobService
    {
        Task GetBlobAsync(string blobContainerName, string localDirectory);

        Task UploadBlobsAsync(string containerName, string directory);
    }
}