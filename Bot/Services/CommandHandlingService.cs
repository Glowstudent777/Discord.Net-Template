using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace Bot.Services;

public class CommandHandlingService
{
    private readonly CommandService _commands;
    private readonly DiscordShardedClient _client;
    private readonly IServiceProvider _services;

    public CommandHandlingService(IServiceProvider services)
    {
        _commands = services.GetRequiredService<CommandService>();
        _client = services.GetRequiredService<DiscordShardedClient>();
        _services = services;

        _commands.CommandExecuted += CommandExecutedAsync;
        _client.MessageReceived += MessageReceivedAsync;
    }

    public async Task InitializeAsync()
    {
        // Register modules that are public and inherit ModuleBase<T>.
        await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
    }

    private async Task MessageReceivedAsync(SocketMessage rawMessage)
    {
        // Ignore system messages, or messages from other bots
        if (rawMessage is not SocketUserMessage { Source: MessageSource.User } message)
            return;

        // This value holds the offset where the prefix ends
        var argPos = 0;

        if (!message.HasMentionPrefix(_client.CurrentUser, ref argPos)) return;

        // Create the websocket context
        var context = new ShardedCommandContext(_client, message);

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

        // the command failed, let's notify the user that something happened.
        await context.Channel.SendMessageAsync($"error: {result}");
    }
}