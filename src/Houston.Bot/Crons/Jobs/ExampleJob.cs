using Quartz;
using System;
using System.Threading.Tasks;
using Houston.Bot.Common;
using Microsoft.Extensions.Logging;
using Discord.WebSocket;
using Houston.Bot.Services;

namespace Houston.Bot.Crons.Jobs;

[DisallowConcurrentExecution]
public class ExampleJob : IJob
{
	private readonly ILogger<ExampleJob> _logger;
	public ExampleJob(ILogger<ExampleJob> logger)
	{
		_logger = logger;
	}

	public Task Execute(IJobExecutionContext context)
	{
		try
		{
			Console.WriteLine("ExampleJob executed");
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error while cleaning up expired keys");
		}
		return Task.CompletedTask;
	}

	public static ITrigger GetTrigger()
	{
		return TriggerBuilder.Create()
			.WithIdentity("ExampleJob-trigger")
			.WithSimpleSchedule(x =>
			{
				x.WithRepeatCount(0);
				x.WithMisfireHandlingInstructionNextWithRemainingCount();
			})
			.StartAt(DateTimeOffset.Now.AddSeconds(15))
			.ForJob("ExampleJob")
			.Build();
	}
}
