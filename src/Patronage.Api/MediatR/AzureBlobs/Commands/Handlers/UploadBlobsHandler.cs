using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.AzureBlobs.Commands.Handlers
{
    public class UploadBlobsHandler : IRequestHandler<UploadBlobsCommand>
    {
        private readonly IBlobService _blobService;

        public UploadBlobsHandler(IBlobService blobService)
        {
            _blobService = blobService;
        }

        public async Task<Unit> Handle(UploadBlobsCommand request, CancellationToken cancellationToken)
        {
            await _blobService.UploadBlobsAsync(request.ContainerName, request.Directory);
            return Unit.Value;
        }
    }
}