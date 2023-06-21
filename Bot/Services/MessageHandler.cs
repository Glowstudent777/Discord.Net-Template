using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Bot.Common;
using Bot.Controllers.Helpers;
using Bot.Models;

namespace Bot.Services;

public class MessageHandler
{
    private static Database Database;
    private readonly DiscordShardedClient _discord;
    private readonly IServiceProvider _services;

    public MessageHandler(IServiceProvider services, Database database)
    {
        _discord = services.GetRequiredService<DiscordShardedClient>();
        _services = services;
        Database = database;

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

        // Add a point to the user's message count
        _ = Task.Run(async () =>
        {

            var channel = (IGuildChannel)message.Channel;
            var userDb = await FindOrCreateUser.Perform(channel.Guild, (IGuildUser)rawMessage.Author, Database);
            userDb.MessageCount++;
            await Database.SaveChangesAsync();
        });

        return Task.CompletedTask;
    }

}