using MediatR;

namespace Patronage.Api.MediatR.AzureBlobs.Commands
{
    public record DownloadBlobsCommand(string BlobContainerName, string LocalDirectory) : IRequest;
}