using Discord;
using Discord.Interactions;
using Houston.Bot.Common;
using Houston.Bot.Components;
using Houston.Database;
using Houston.Database.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Houston.Bot.Modules.Interactions;

public class GeneralModule : InteractionsBase
{
	public GeneralModule() { }

	[SlashCommand("ping", "Display bot latency")]
	public async Task LatencyAsync()
	{
		await DeferAsync();

		var message = await FollowupAsync("📡 Ping 1");

		await message.ModifyAsync(x => x.Content = "📡 Ping 2");
		var latency = (message.CreatedAt.Millisecond - message.EditedTimestamp.Value.Millisecond);

		while (latency < 0)
		{
			var initLat = (message.EditedTimestamp.Value.Millisecond);
			await message.ModifyAsync(x => x.Content = "Another ping???");
			var finalLat = (message.EditedTimestamp.Value.Millisecond);
			latency = (finalLat - initLat);
		}

		await message.ModifyAsync(x =>
		{
			x.Content = $"⏱️ Message Latency: {latency}\n🛰️ Websocket Latency: {Context.Client.Latency}";
		});
	}

	[SlashCommand("about", "Info about our service")]
	public async Task About()
	{
		await DeferAsync();

		var shard = Context.Client.GetShardFor(Context.Guild).ShardId;

		var AboutButtons = new ActionRowBuilder()
			.AddComponent(ButtonComponents.LinkButton("Support Server", Config.SupportServer));

		var actionRow = new ComponentBuilder()
			.AddRow(AboutButtons)
			.Build();

		var embed = new EmbedBuilder()
			.WithTitle("Welcomer")
			.WithDescription(
				$"I'm a bot :).\n\n**Latency** {Context.Client.Latency}ms (Shard #{shard})\n**Version** {Config.Version}")
			.WithColor(Config.Colors.Invisible)
			.WithImageUrl($"{Config.EmbedSpacer}")
			.Build();

		var embed2 = new EmbedBuilder()
			.WithTitle($"Latest Release - {Config.Version}")
			// Put each release note on a new line and prefix it with a dash
			.WithDescription(string.Join("\n", Config.ReleaseNotes.Select(x => $"- {x}")))
			.WithColor(Config.Colors.Invisible)
			.WithImageUrl($"{Config.EmbedSpacer}")
			.WithFooter("© 2022 - 2024 Glowstudent. All Rights Reserved.")
			.Build();

		await FollowupAsync(embeds: new[] { embed, embed2 }, components: actionRow);
	}
}
