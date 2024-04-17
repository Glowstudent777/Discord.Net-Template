using Discord;
using Discord.Commands;
using Houston.Bot.Preconditions;
using Houston.Bot.Common;

namespace Houston.Bot.Modules.Text;

public class AdminModule : ModuleBase<ShardedCommandContext>
{
	[RequireBotAdmin]
	[Command("debug")]
	[Summary("Debug")]
	public async Task DebugAsync()
	{
		var message = await ReplyAsync("Debugging...");

		var version = DiscordConfig.Version;
		var guild = Context.Guild;
		var shard = Context.Client.GetShardFor(Context.Guild).ShardId;

		// TODO: Send to a hastebin
		var guildPerms = Permissions.ParseGuild(Context.Guild.CurrentUser.GuildPermissions);

		await message.ModifyAsync(x => x.Content = $"API Version: {version}\nShard: {shard}\nGuild ID: {guild.Id}\nCached Guild Count: {Context.Client.Guilds.Count}\nPermissions:\n{string.Join("\n", guildPerms.Select(x => $"- {x}"))}");
	}
}
