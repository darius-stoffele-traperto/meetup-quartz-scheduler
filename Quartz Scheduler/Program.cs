using System.Collections.Specialized;
using Quartz_Scheduler;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;

namespace QuartzSampleApp
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            var properties = new NameValueCollection
            {
                ["quartz.scheduler.instanceName"] = "QuartzScheduler",
                ["quartz.scheduler.instanceId"] = "AUTO",
                ["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX",
                ["quartz.jobStore.useProperties"] = "true",
                ["quartz.jobStore.dataSource"] = "default",
                ["quartz.jobStore.tablePrefix"] = "QRTZ_",
                ["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.MySQLDelegate, Quartz",
                ["quartz.dataSource.default.connectionString"] = "Server=localhost;Database=Quartz;User Id=root;Password=Dada2002;",
                ["quartz.dataSource.default.provider"] = "MySqlConnector",
                ["quartz.serializer.type"] = "json"
            };

            LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());

            // Grab the Scheduler instance from the Factory
            StdSchedulerFactory factory = new StdSchedulerFactory(properties);
            IScheduler scheduler = await factory.GetScheduler();
            
            await scheduler.Start();
            
            // Erstelle Jobs
            IJobDetail helloJob = JobFactory.CreateHelloJob();
            IJobDetail dailyJob = JobFactory.CreateDailyJob();
        
            // Erstelle Trigger
            ITrigger trigger1 = TriggerFactory.CreateSimpleTrigger();
            ITrigger cronTrigger1 = TriggerFactory.CreateCronTrigger1();
            ITrigger cronTrigger2 = TriggerFactory.CreateCronTrigger2();
            ITrigger cronTrigger3 = TriggerFactory.CreateCronTrigger3();
        
            // Planen der Jobs, wenn sie noch nicht existieren
            await ScheduleJobIfNotExists(scheduler, helloJob, trigger1);
            await ScheduleJobIfNotExists(scheduler, dailyJob, cronTrigger1);

            await Task.Delay(TimeSpan.FromSeconds(30));
            
            await scheduler.Shutdown();

            Console.WriteLine("Press any key to close the application");
            Console.ReadKey();
        }
        private static async Task ScheduleJobIfNotExists(IScheduler scheduler, IJobDetail job, ITrigger trigger)
        {
            if (!await scheduler.CheckExists(job.Key) || !await scheduler.CheckExists(trigger.Key))
            {
                Console.WriteLine($"Erstelle Job: {job.Key}");
                await scheduler.ScheduleJob(job, trigger);
            }
            else
            {
                Console.WriteLine($"Job {job.Key} existiert bereits.");
            }
        }
    }
}
