using Quartz;
using Quartz.Impl;
using Houston.Bot.Crons;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz.Spi;
using System;

namespace Houston.Bot.Services;

public class JobSchedulerService
{
	private readonly IScheduler _scheduler;
	private readonly IJobFactory _jobFactory;
	private readonly IServiceProvider _serviceProvider;

	public JobSchedulerService(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
		_scheduler = new StdSchedulerFactory().GetScheduler().GetAwaiter().GetResult();
		_jobFactory = new JobFactory(serviceProvider);
	}

	public async Task Start()
	{
		_scheduler.JobFactory = _jobFactory;
		await _scheduler.Start();

		var scheduleJobs = _serviceProvider.GetRequiredService<ScheduleJobs>();
		await scheduleJobs.ScheduleAllJobs();
	}

	public async Task Stop()
	{
		await _scheduler.Shutdown();
	}

	public async Task PauseJob(string jobName)
	{
		var jobKey = new JobKey(jobName);
		await _scheduler.PauseJob(jobKey);
	}

	public async Task ResumeJob(string jobName)
	{
		var jobKey = new JobKey(jobName);
		await _scheduler.ResumeJob(jobKey);
	}

	public async Task DeleteJob(string jobName)
	{
		var jobKey = new JobKey(jobName);
		await _scheduler.DeleteJob(jobKey);
	}

	public async Task ScheduleJob<T>(string jobName, ITrigger trigger) where T : IJob
	{
		IJobDetail job = JobBuilder.Create<T>()
			.WithIdentity(jobName)
			.Build();

		await _scheduler.ScheduleJob(job, trigger);
	}
}
