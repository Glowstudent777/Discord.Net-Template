using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Spi;

namespace Houston.Bot.Crons;

public class JobFactory : IJobFactory
{
	private readonly IServiceProvider _serviceProvider;

	public JobFactory(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
	{
		var jobDetail = bundle.JobDetail;
		var jobType = jobDetail.JobType;
		var jobInstance = _serviceProvider.GetService(jobType);

		if (jobInstance == null || !(jobInstance is IJob))
		{
			throw new SchedulerException($"Failed to create job of type {jobType.FullName}");
		}

		return jobInstance as IJob;
	}

	public void ReturnJob(IJob job)
	{
	}
}
