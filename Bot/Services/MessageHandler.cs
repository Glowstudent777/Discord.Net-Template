using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Bot.Common;

namespace Bot.Services;

public class MessageHandler
{
    private readonly DiscordShardedClient _discord;
    private readonly IServiceProvider _services;

    public MessageHandler(IServiceProvider services)
    {
        _discord = services.GetRequiredService<DiscordShardedClient>();
        _services = services;

        _discord.MessageReceived += MessageReceivedAsync;
    }

    public static Task MessageReceivedAsync(SocketMessage rawMessage)
    {
        if (rawMessage is not SocketUserMessage message)
            return Task.CompletedTask;

        if (!(rawMessage.Source is MessageSource.User))
            return Task.CompletedTask;

        if (!(rawMessage.Channel is IGuildChannel))
            return Task.CompletedTask;

        return Task.CompletedTask;
    }

}