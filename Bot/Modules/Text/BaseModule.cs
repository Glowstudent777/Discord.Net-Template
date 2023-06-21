using Discord.Commands;
using Bot.Controllers.Helpers;
using Discord;

namespace Bot.Modules.Text;

public class BaseModule : ModuleBase<ShardedCommandContext>
{
    private readonly Database Database;
    public BaseModule(Database database)
    {
        Database = database;
    }

    [Command("ping")]
    [Summary("Pong!")]
    public Task PongAsync()
        => ReplyAsync("Pong!");
}