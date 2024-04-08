using Houston.Bot.Crons.Jobs;
using Houston.Bot.Services;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Houston.Bot.Crons;

public class ScheduleJobs
{
	private readonly JobSchedulerService _jobSchedulerService;
	private readonly IServiceProvider _serviceProvider;

	public ScheduleJobs(JobSchedulerService jobSchedulerService, IServiceProvider serviceProvider)
	{
		_jobSchedulerService = jobSchedulerService;
		_serviceProvider = serviceProvider;
	}

	public async Task ScheduleAllJobs()
	{
		await _jobSchedulerService.ScheduleJob<ExampleJob>("ExampleJob", ExampleJob.GetTrigger());
	}
}
