using Quartz;

namespace Quartz_Scheduler;

public class TriggerFactory
{
    public static ITrigger CreateSimpleTrigger()
    {
        return TriggerBuilder.Create()
            .WithIdentity("trigger1", "group1")
            .StartNow()
            .WithSimpleSchedule(x => x
                .WithIntervalInSeconds(5)
                .RepeatForever())
            .Build();
    }

    public static ITrigger CreateCronTrigger1()
    {
        return TriggerBuilder.Create()
            .WithIdentity("trigger1", "group2")
            .StartNow()
            .WithCronSchedule("0 30 10 ? * MON-FRI") // Der Cron-Trigger wird den Job um 9:00 Uhr an jedem Werktag ausführen.
            .Build();
    }

    public static ITrigger CreateCronTrigger2()
    {
        return TriggerBuilder.Create()
            .WithIdentity("trigger2", "group2")
            .StartNow()
            .WithCronSchedule("0 0/30 8-9 5,20 * ?") // Der Cron-Trigger wird den Job alle 30 Minuten zwischen 8:00 und 9:59 Uhr an jedem 5. und 20. des Monats ausführen.
            .Build();
    }

    public static ITrigger CreateCronTrigger3()
    {
        return TriggerBuilder.Create()
            .WithIdentity("trigger3", "group2")
            .StartNow()
            .WithCronSchedule("0 30 10-13 ? * WED,FRI") // Der Cron-Trigger wird den Job immer um 10:30, 11:30, 12:30 und 13:30 an jedem Mittwoch (WED) und Freitag (FRI) ausführen, zwischen 10:00 und 13:59 Uhr.
            .Build();
    }
}