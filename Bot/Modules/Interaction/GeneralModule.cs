using Discord.Interactions;
using Bot.Common;
using Discord;

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
    }
}