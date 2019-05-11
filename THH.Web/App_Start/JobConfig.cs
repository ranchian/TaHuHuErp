using Quartz;
using System.Configuration;
using THH.Web.TaskJobs;

namespace THH.Web.App_Start
{
    public class JobConfig
    {
        private static IScheduler _scheduler;

        public static async void Start()
        {
            _scheduler = await Quartz.Impl.StdSchedulerFactory.GetDefaultScheduler();

            await _scheduler.Start();

            var cronStr = ConfigurationManager.AppSettings["DownLoadFilesJobCron"].Trim();
            IJobDetail downloadJob = JobBuilder.Create<DownLoadFilesJob>().WithIdentity("DownLoadFilesJob", "MinuteGroup").Build();

            ITrigger downloadTrigger = TriggerBuilder.Create()
                .WithIdentity("DownLoadFilesJob", "MinuteGroup")
                .WithCronSchedule(cronStr)
                .ForJob(downloadJob)
                .Build();

            await _scheduler.ScheduleJob(downloadJob, downloadTrigger);

            IJobDetail keepliveJob = JobBuilder.Create<KeepliveJob>().WithIdentity("KeepliveJob", "KeepliveGroup").Build();
            ITrigger keepliveTrigger = TriggerBuilder.Create()
                .WithIdentity("KeepliveJob", "KeepliveGroup")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(100).RepeatForever()).ForJob(keepliveJob)
                .Build();

            await _scheduler.ScheduleJob(keepliveJob, keepliveTrigger);

        }

        public static async void Stop()
        {
            if (_scheduler != null)
            {
                await _scheduler.Shutdown();
            }
        }
    }
}