using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using Houston.Bot.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using Houston.Database;
using Discord.Addons.Hosting;
using Microsoft.Extensions.Logging;
using Houston.Bot.Crons;
using Quartz;
using Houston.Bot.Crons.Jobs;

namespace Houston.Bot;

internal static class Program
{
	private static async Task Main(string[] args)
	{
		var host = Host.CreateDefaultBuilder(args)
			.UseSystemd()
			.UseSerilog()
			.ConfigureServices(ConfigureServices)
			.ConfigureDiscordShardedHost((context, config) =>
			{
				config.Token = Environment.GetEnvironmentVariable("TOKEN");
			})
			.ConfigureLogging((context, logging) =>
			{
				logging.ClearProviders();
			})
			.Build();

		var jobSchedulerService = host.Services.GetRequiredService<JobSchedulerService>();
		await jobSchedulerService.Start();

		Log.Logger = new LoggerConfiguration()
			.ReadFrom.Configuration(host.Services.GetRequiredService<IConfiguration>())
			.CreateLogger();

		try
		{
			if (args.Contains("--skip-migration"))
			{
				Log.Information("Skipping database migration");
			}
			else
			{
				Log.Information("Migrating database");
				using var scope = host.Services.CreateScope();
				var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
				await db.Database.MigrateAsync();
			}

			Log.Information("Running host");
			await host.RunAsync();
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
			Log.Fatal(ex, "Host terminated unexpectedly");
		}
		finally
		{
			Log.CloseAndFlush();
		}
	}

	private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
	{
		services.AddSingleton(typeof(Microsoft.Extensions.Logging.ILogger<>), typeof(Services.Logger<>));

		services.AddDbContext<DatabaseContext>();

		services.AddSingleton<InteractionService>();
		services.AddSingleton<CommandService>();
		services.AddHostedService<InteractionHandlingService>();
		services.AddHostedService<CommandHandlingService>();

		services.AddQuartz(q => q.UseJobFactory<JobFactory>());
		services.AddSingleton<JobSchedulerService>();
		services.AddSingleton<ScheduleJobs>();

		services.AddSingleton<ExampleJob>();

		services.AddHostedService<BotService>();
	}
}