using Discord.Interactions;
using Bot.Common;
using Bot.Controllers.Helpers;
using Discord;
using System.Data.Entity;

namespace Bot.Modules.Interaction;

public class GeneralModule
{
    public class GeneralGroup : InteractionsBase
    {
        public GeneralGroup(Database database) : base(database) { }

        [SlashCommand("ping", "Display bot latency")]
        public async Task LatencyAsync()
        {
            await DeferAsync();

            int latency = Context.Client.Latency;

            var embed = new EmbedBuilder()
                .WithTitle("Latency")
                .WithDescription($"⌛ {latency} ms")
                .WithColor(Config.Colors.Primary)
                .Build();

            await FollowupAsync(embed: embed);
        }

        [SlashCommand("messagecount", "Display your message count")]
        public async Task MessageCountAsync()
        {
            await DeferAsync();

            var userDb = await FindOrCreateUser.Perform(Context.Guild, (IGuildUser)Context.User, _database);

            var embed = new EmbedBuilder()
                .WithTitle("Message Count")
                .WithDescription($"📝 {userDb.MessageCount}")
                .WithColor(Config.Colors.Primary)
                .Build();

            await FollowupAsync(embed: embed);
        }
    }
} 