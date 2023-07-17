using Discord.Interactions;
using Bot.Common;
using Bot.Controllers.Helpers;
using Discord;
using System.Data.Entity;
using Bot.Components;

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

        [SlashCommand("buttons", "Display buttons")]
        public async Task DisplayButtons()
        {
            await DeferAsync();

            var buttons = new ActionRowBuilder()
                .AddComponent(ButtonComponents.ToggleButton("Toggle Me", "toggle", false, ButtonStyle.Success))
                .AddComponent(ButtonComponents.TriButton("Cycle Me", "cycle", false, ButtonStyle.Success));

            var actionRow = new ComponentBuilder()
                .AddRow(buttons)
                .Build();

            var embed = new EmbedBuilder()
                    .WithTitle("Buttons")
                    .WithDescription("Click the buttons below")
                    .WithColor(Config.Colors.Primary)
                    .Build();

            await FollowupAsync(embed: embed, components: actionRow);

        }
    }
}