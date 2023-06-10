using System.Reflection;
using System.Windows.Input;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Bot.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bot.Services;

public class InteractionHandlingService
{
    private readonly InteractionService _interactions;
    private readonly DiscordShardedClient _client;
    private readonly IServiceProvider _services;

    public InteractionHandlingService(IServiceProvider services)
    {
        _interactions = services.GetRequiredService<InteractionService>();
        _client = services.GetRequiredService<DiscordShardedClient>();
        _services = services;
    }

    public void Initialize()
    {
        // process the InteractionCreated payloads to execute Interactions commands
        _client.InteractionCreated += HandleInteraction;

        // process the command execution results 
        _interactions.SlashCommandExecuted += SlashCommandExecuted;
        _interactions.ContextCommandExecuted += ContextCommandExecuted;
        _interactions.ComponentCommandExecuted += ComponentCommandExecuted;
    }

    private Task ComponentCommandExecuted(ComponentCommandInfo arg1, Discord.IInteractionContext arg2, IResult arg3)
    {
        if (!arg3.IsSuccess)
        {
            switch (arg3.Error)
            {
                case InteractionCommandError.UnmetPrecondition:
                    // implement
                    break;
                case InteractionCommandError.UnknownCommand:
                    // implement
                    break;
                case InteractionCommandError.BadArgs:
                    // implement
                    break;
                case InteractionCommandError.Exception:
                    // implement
                    break;
                case InteractionCommandError.Unsuccessful:
                    // implement
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
                    // implement
                    break;
                case InteractionCommandError.UnknownCommand:
                    // implement
                    break;
                case InteractionCommandError.BadArgs:
                    // implement
                    break;
                case InteractionCommandError.Exception:
                    // implement
                    break;
                case InteractionCommandError.Unsuccessful:
                    // implement
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
            switch (arg3.Error)
            {
                case InteractionCommandError.UnmetPrecondition:
                    await arg2.Interaction.RespondAsync($"Unmet Precondition: {arg3.ErrorReason}", ephemeral: true);
                    break;
                case InteractionCommandError.UnknownCommand:
                    await arg2.Interaction.RespondAsync("Unknown command", ephemeral: true);
                    break;
                case InteractionCommandError.BadArgs:
                    await arg2.Interaction.RespondAsync("Invalid number or arguments", ephemeral: true);
                    break;
                case InteractionCommandError.Exception:
                    Console.WriteLine("Command Error:");
                    Console.WriteLine(arg3.ErrorReason);
                    await arg2.Interaction.RespondAsync($"Command exception: {arg3.ErrorReason}. If this message persists, please let us know in the support server (https://discord.gg/xyzMyJH) !", ephemeral: true);
                    break;
                case InteractionCommandError.Unsuccessful:
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
            // create an execution context that matches the generic type parameter of your InteractionModuleBase<T> modules
            var ctx = new ShardedInteractionContext(_client, arg);
            await _interactions.ExecuteCommandAsync(ctx, _services);
            Console.WriteLine("processed interaction!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Caught exception:");
            Console.WriteLine(ex);
            // if a Slash Command execution fails it is most likely that the original interaction acknowledgement will persist. It is a good idea to delete the original
            // response, or at least let the user know that something went wrong during the command execution.
            if (arg.Type == InteractionType.ApplicationCommand)
            {
                await arg.GetOriginalResponseAsync().ContinueWith(async (msg) => await msg.Result.DeleteAsync());
            }
        }
    }
}