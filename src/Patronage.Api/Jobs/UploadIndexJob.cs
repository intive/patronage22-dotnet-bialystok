using Patronage.Contracts.Interfaces;
using Quartz;

namespace Patronage.Api.Jobs
{
    public class UploadIndexJob : IJob
    {
        public readonly IBlobService _blobService;

        public UploadIndexJob(IBlobService blobService)
        {
            _blobService = blobService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _blobService.UploadBlobsAsync("luceneIndex", "index");
        }
    }
}