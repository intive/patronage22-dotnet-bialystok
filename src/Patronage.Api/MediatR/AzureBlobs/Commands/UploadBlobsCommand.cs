using MediatR;

namespace Patronage.Api.MediatR.AzureBlobs.Commands
{
    public record UploadBlobsCommand(string ContainerName, string Directory) : IRequest;
}