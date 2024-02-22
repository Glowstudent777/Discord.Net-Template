using System;
using System.Reflection;
using Discord;
using Discord.Addons.Hosting;
using Discord.Addons.Hosting.Util;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Houston.Bot.Services;

public class InteractionHandlingService : DiscordShardedClientService
{
	private readonly IServiceProvider _services;
	private readonly InteractionService _interactions;
	private readonly IHostEnvironment _env;
	private readonly IConfiguration _config;
	private readonly ILogger _logger;

	public InteractionHandlingService(DiscordShardedClient client, ILogger<InteractionHandlingService> logger, IServiceProvider services, InteractionService interactions, IHostEnvironment environment, IConfiguration configuration) : base(client, logger)
	{
		_services = services;
		_interactions = interactions;
		_env = environment;
		_config = configuration;
		_logger = logger;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		Client.InteractionCreated += HandleInteraction;

		_interactions.SlashCommandExecuted += SlashCommandExecuted;
		_interactions.ContextCommandExecuted += ContextCommandExecuted;
		_interactions.ComponentCommandExecuted += ComponentCommandExecuted;

		await _interactions.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
		await Client.WaitForReadyAsync(stoppingToken);

		await _interactions.AddModulesGloballyAsync(true, _interactions.Modules.ToArray());
	}

	private Task ComponentCommandExecuted(ComponentCommandInfo arg1, Discord.IInteractionContext arg2, IResult arg3)
	{
		if (!arg3.IsSuccess)
		{
			switch (arg3.Error)
			{
				case InteractionCommandError.UnmetPrecondition:
					if (arg2.Interaction.HasResponded)
						arg2.Interaction.FollowupAsync($"Unmet Precondition: {arg3.ErrorReason}");
					else
						arg2.Interaction.RespondAsync($"Unmet Precondition: {arg3.ErrorReason}", ephemeral: true);
					break;
				case InteractionCommandError.UnknownCommand:
					if (arg2.Interaction.HasResponded)
						arg2.Interaction.FollowupAsync("Unknown command", ephemeral: true);
					else
						arg2.Interaction.RespondAsync("Unknown command", ephemeral: true);
					break;
				case InteractionCommandError.BadArgs:
					if (arg2.Interaction.HasResponded)
						arg2.Interaction.FollowupAsync("Invalid number or arguments");
					else
						arg2.Interaction.RespondAsync("Invalid number or arguments", ephemeral: true);
					break;
				case InteractionCommandError.Exception:
					if (arg2.Interaction.HasResponded)
						arg2.Interaction.FollowupAsync("Command could not be executed");
					else
						arg2.Interaction.RespondAsync("Command could not be executed", ephemeral: true);
					break;
				case InteractionCommandError.Unsuccessful:
					if (arg2.Interaction.HasResponded)
						arg2.Interaction.FollowupAsync("Command could not be executed");
					else
						arg2.Interaction.RespondAsync("Command could not be executed", ephemeral: true);
					break;
				default:
					break;
			}
		}
		return Task.CompletedTask;
	}

	private Task ContextCommandExecuted(ContextCommandInfo arg1, Discord.IInteractionContext arg2, IResult arg3)
	{
		if (!arg3.IsSuccess)
		{
			switch (arg3.Error)
			{
				case InteractionCommandError.UnmetPrecondition:
					if (arg2.Interaction.HasResponded)
						arg2.Interaction.FollowupAsync($"Unmet Precondition: {arg3.ErrorReason}");
					else
						arg2.Interaction.RespondAsync($"Unmet Precondition: {arg3.ErrorReason}", ephemeral: true);
					break;
				case InteractionCommandError.UnknownCommand:
					if (arg2.Interaction.HasResponded)
						arg2.Interaction.FollowupAsync("Unknown command", ephemeral: true);
					else
						arg2.Interaction.RespondAsync("Unknown command", ephemeral: true);
					break;
				case InteractionCommandError.BadArgs:
					if (arg2.Interaction.HasResponded)
						arg2.Interaction.FollowupAsync("Invalid number or arguments");
					else
						arg2.Interaction.RespondAsync("Invalid number or arguments", ephemeral: true);
					break;
				case InteractionCommandError.Exception:
					if (arg2.Interaction.HasResponded)
						arg2.Interaction.FollowupAsync("Command could not be executed");
					else
						arg2.Interaction.RespondAsync("Command could not be executed", ephemeral: true);
					break;
				case InteractionCommandError.Unsuccessful:
					if (arg2.Interaction.HasResponded)
						arg2.Interaction.FollowupAsync("Command could not be executed");
					else
						arg2.Interaction.RespondAsync("Command could not be executed", ephemeral: true);
					break;
				default:
					break;
			}
		}
		return Task.CompletedTask;
	}

	private async Task SlashCommandExecuted(SlashCommandInfo arg1, Discord.IInteractionContext arg2, Discord.Interactions.IResult arg3)
	{
		if (!arg3.IsSuccess)
		{
			_logger.LogError($"Error executing slash command: {arg3.ErrorReason}");
			switch (arg3.Error)
			{
				case InteractionCommandError.UnmetPrecondition:
					if (arg2.Interaction.HasResponded)
						await arg2.Interaction.FollowupAsync($"Unmet Precondition: {arg3.ErrorReason}");
					else
						await arg2.Interaction.RespondAsync($"Unmet Precondition: {arg3.ErrorReason}", ephemeral: true);
					break;
				case InteractionCommandError.UnknownCommand:
					if (arg2.Interaction.HasResponded)
						await arg2.Interaction.FollowupAsync("Unknown command", ephemeral: true);
					else
						await arg2.Interaction.RespondAsync("Unknown command", ephemeral: true);
					break;
				case InteractionCommandError.BadArgs:
					if (arg2.Interaction.HasResponded)
						await arg2.Interaction.FollowupAsync("Invalid number or arguments");
					else
						await arg2.Interaction.RespondAsync("Invalid number or arguments", ephemeral: true);
					break;
				case InteractionCommandError.Exception:
					if (arg2.Interaction.HasResponded)
						await arg2.Interaction.FollowupAsync("Command could not be executed");
					else
						await arg2.Interaction.RespondAsync("Command could not be executed", ephemeral: true);
					break;
				case InteractionCommandError.Unsuccessful:
					if (arg2.Interaction.HasResponded)
						await arg2.Interaction.FollowupAsync("Command could not be executed");
					else
						await arg2.Interaction.RespondAsync("Command could not be executed", ephemeral: true);
					break;
				default:
					break;
			}
		}
		return;
	}

	private async Task HandleInteraction(SocketInteraction arg)
	{
		try
		{
			var ctx = new ShardedInteractionContext(Client, arg);
			await _interactions.ExecuteCommandAsync(ctx, _services);
		}
		catch (Exception ex)
		{
			Console.WriteLine("Caught exception:");
			Console.WriteLine(ex);

			if (arg.Type == InteractionType.ApplicationCommand)
			{
				await arg.GetOriginalResponseAsync().ContinueWith(async (msg) => await msg.Result.DeleteAsync());
				await arg.RespondAsync("We encountered an error while processing your command.", ephemeral: true);
			}
		}
	}
}
