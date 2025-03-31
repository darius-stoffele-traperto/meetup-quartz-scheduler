using Quartz;

namespace Quartz_Scheduler;

public class DailyJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await Console.Out.WriteLineAsync("Guten Morgen! Es ist " + DateTime.Now.ToLongTimeString());
    }
}