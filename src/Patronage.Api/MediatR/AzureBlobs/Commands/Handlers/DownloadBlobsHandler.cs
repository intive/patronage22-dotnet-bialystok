using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.AzureBlobs.Commands.Handlers
{
    public class DownloadBlobsHandler : IRequestHandler<DownloadBlobsCommand>
    {
        private readonly IBlobService _blobService;

        public DownloadBlobsHandler(IBlobService blobService)
        {
            _blobService = blobService;
        }

        public async Task<Unit> Handle(DownloadBlobsCommand request, CancellationToken cancellationToken)
        {
            await _blobService.GetBlobAsync(request.BlobContainerName, request.LocalDirectory);
            return Unit.Value;
        }
    }
}