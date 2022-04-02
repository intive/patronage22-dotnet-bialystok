using Patronage.Api.Jobs;
using Patronage.Contracts.Helpers;
using Patronage.Contracts.Interfaces;
using Quartz;
using Quartz.Impl;

namespace Patronage.Api
{
    public static class LuceneManager
    {
        public static async Task Initialize(IBlobService blobService)
        {
            await blobService.GetBlobAsync("luceneindex", LuceneFieldNames.IndexName);

            var factory = new StdSchedulerFactory();

            IScheduler scheduler = await factory.GetScheduler();

            await scheduler.Start();

            var job = JobBuilder.Create<UploadIndexJob>()
                .WithIdentity("uploadLucene", "luceneGroup")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("luceneTrigger", "luceneGroup")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(30)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        public static async Task Upload(IBlobService blobService)
        {
            await blobService.UploadBlobsAsync("luceneindex", "index");
        }
    }
}