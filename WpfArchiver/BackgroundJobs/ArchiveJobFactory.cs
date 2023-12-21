namespace WpfArchiver.Infrastructure.BackgroundJobs;

using Quartz;
using Quartz.Spi;
using System;

public class ArchiveJobFactory : IJobFactory
{
    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        throw new NotImplementedException();
    }

    public void ReturnJob(IJob job)
    {
        throw new NotImplementedException();
    }
}
