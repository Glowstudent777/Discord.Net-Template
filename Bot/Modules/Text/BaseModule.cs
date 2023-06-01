using Discord.Commands;

namespace Bot.Modules.Text;

public class BaseModule : ModuleBase<ShardedCommandContext>
{
    [Command("ping")]
    [Summary("Pong!")]
    public Task PongAsync()
        => ReplyAsync("Pong!");
}