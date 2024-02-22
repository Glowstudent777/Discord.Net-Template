using Discord;
using Discord.Interactions;
using Houston.Database;
using Houston.Database.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Houston.Bot.Modules.Interactions;

[Group("reputation", "User Repuations")]
public class ReputationGroupModule : InteractionsBase
{
	private readonly DatabaseContext _dbContext;
	public ReputationGroupModule(DatabaseContext dbContext)
	{
		_dbContext = dbContext;
	}

	[SlashCommand("view", "View another members repuation")]
	public async Task RepViewAsync(IUser user)
	{
		await DeferAsync();

		var repMember = await _dbContext.ReputationMembers.GetForMemberAsync(Context.Guild.Id, user.Id);
		var rep = repMember?.Reputation ?? 0;

		var embed = new EmbedBuilder()
			.WithTitle($"{user.Username}'s Reputation")
			.WithDescription($"Reputation: {rep}")
			.WithColor(Color.Blue)
			.Build();

		await FollowupAsync(embeds: new[] { embed });
	}
}