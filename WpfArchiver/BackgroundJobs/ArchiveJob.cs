namespace WpfArchiver.BackgroundJobs;

using Quartz;
using System;
using System.Threading.Tasks;

public class ArchiveJob : IJob
{
  public const string JobName = "ArchiveJob";
  public const string GroupName = "ArchiveJobGroupName";

  public Task Execute(IJobExecutionContext context)
  {
    throw new NotImplementedException();
  }
}

