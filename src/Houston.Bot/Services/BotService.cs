using Discord.Addons.Hosting;
using Discord.Addons.Hosting.Util;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Discord;
using System.Reflection;

namespace Houston.Bot.Services;

public class BotService : DiscordShardedClientService
{
	private readonly ILogger<BotService> _logger;

	public BotService(DiscordShardedClient client, ILogger<BotService> logger) : base(client, logger)
	{
		_logger = logger;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		await Client.WaitForReadyAsync(stoppingToken);

		await Client.SetActivityAsync(new Game("with the universe"));

		_logger.LogInformation("Services started");
	}
}
