using Quartz;

namespace Quartz_Scheduler;

public class JobFactory
{
    public static IJobDetail CreateHelloJob()
    {
        return JobBuilder.Create<HelloJob>()
            .WithIdentity("job1", "group1")
            .Build();
    }

    public static IJobDetail CreateDailyJob()
    {
        return JobBuilder.Create<DailyJob>()
            .WithIdentity("job1", "group2")
            .Build();
    }
}