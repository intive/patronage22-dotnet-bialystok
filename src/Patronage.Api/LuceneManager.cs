using Patronage.Contracts.Helpers;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api
{
    public static class LuceneManager
    {
        public static async Task Initialize(IBlobService blobService)
        {
            Directory.CreateDirectory($@"{LuceneFieldNames.IndexName}");
            await blobService.GetBlobAsync("luceneindex", "./");
        }

        public static async Task Upload(IBlobService blobService)
        {
            await blobService.UploadBlobsAsync("luceneindex", LuceneFieldNames.IndexName);
        }
    }
}