using Houston.Bot.Common;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;
using Discord.Addons.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Houston.Bot.Services;

public class CommandHandlingService : DiscordShardedClientService
{
	private readonly CommandService _commands;
	private readonly IServiceProvider _services;
	private readonly IHostEnvironment _env;
	private readonly IConfiguration _config;
	private readonly ILogger _logger;

	public CommandHandlingService(DiscordShardedClient client, ILogger<InteractionHandlingService> logger, IServiceProvider services, CommandService commands, IHostEnvironment environment, IConfiguration configuration) : base(client, logger)
	{
		_services = services;
		_commands = commands;
		_env = environment;
		_config = configuration;
		_logger = logger;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		_commands.CommandExecuted += CommandExecutedAsync;
		Client.MessageReceived += MessageReceivedAsync;

		await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
	}

	private async Task MessageReceivedAsync(SocketMessage rawMessage)
	{
		// Ignore system messages, or messages from other bots
		if (rawMessage is not SocketUserMessage { Source: MessageSource.User } message)
			return;

		// This value holds the offset where the prefix ends
		var argPos = 0;

		if (!message.HasMentionPrefix(Client.CurrentUser, ref argPos)) return;

		// Create the websocket context
		var context = new ShardedCommandContext(Client, message);

		await _commands.ExecuteAsync(context, argPos, _services);
	}

	private static async Task CommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
	{
		// command is unspecified when there was a search failure (command not found); we don't care about these errors
		if (!command.IsSpecified)
			return;

		// the command was successful, we don't care about this result, unless we want to log that a command succeeded.
		if (result.IsSuccess)
			return;

		switch (result.Error)
		{
			case CommandError.UnknownCommand:
				// implement
				break;
			case CommandError.BadArgCount:
				// implement
				break;
			case CommandError.UnmetPrecondition:
				// implement
				break;
			case CommandError.ParseFailed:
				// implement
				break;
			case CommandError.ObjectNotFound:
				// implement
				break;
			case CommandError.MultipleMatches:
				// implement
				break;
			case CommandError.Exception:
				await GenerateMessage.Error(context, title: $"Command exception: {result.ErrorReason}.", description: "If this message persists, please let us know in the support server!", supportinvite: true);
				break;
			case CommandError.Unsuccessful:
				// implement
				break;
			case null:
				// implement
				break;
			default:
				break;
		}

		// the command failed, let's notify the user that something happened.
		//await context.Channel.SendMessageAsync($"error: {result}");
	}
}