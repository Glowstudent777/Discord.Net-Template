using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Houston.Bot.Common;

public class Permissions
{
	public static string[] ParseGuild(GuildPermissions guildPerms)
	{
		var perms = new List<string>();

		// Add in alphabetical order
		if (guildPerms.Administrator) { perms.Add("Administrator"); }
		if (guildPerms.AddReactions) { perms.Add("Add Reactions"); }
		if (guildPerms.AttachFiles) { perms.Add("Attach Files"); }
		if (guildPerms.BanMembers) { perms.Add("Ban Members"); }
		if (guildPerms.ChangeNickname) { perms.Add("Change Nickname"); }
		if (guildPerms.Connect) { perms.Add("Connect"); }
		if (guildPerms.CreateInstantInvite) { perms.Add("Create Instant Invite"); }
		if (guildPerms.DeafenMembers) { perms.Add("Deafen Members"); }
		if (guildPerms.EmbedLinks) { perms.Add("Embed Links"); }
		if (guildPerms.KickMembers) { perms.Add("Kick Members"); }
		if (guildPerms.ManageChannels) { perms.Add("Manage Channels"); }
		if (guildPerms.ManageEmojisAndStickers) { perms.Add("Manage Emojis and Stickers"); }
		if (guildPerms.ManageGuild) { perms.Add("Manage Guild"); }
		if (guildPerms.ManageMessages) { perms.Add("Manage Messages"); }
		if (guildPerms.ManageNicknames) { perms.Add("Manage Nicknames"); }
		if (guildPerms.ManageRoles) { perms.Add("Manage Roles"); }
		if (guildPerms.ManageThreads) { perms.Add("Manage Threads"); }
		if (guildPerms.ManageWebhooks) { perms.Add("Manage Webhooks"); }
		if (guildPerms.MentionEveryone) { perms.Add("Mention Everyone"); }
		if (guildPerms.MentionEveryone) { perms.Add("Mention Everyone"); }
		if (guildPerms.MoveMembers) { perms.Add("Move Members"); }
		if (guildPerms.MuteMembers) { perms.Add("Mute Members"); }
		if (guildPerms.PrioritySpeaker) { perms.Add("Priority Speaker"); }
		if (guildPerms.ReadMessageHistory) { perms.Add("Read Message History"); }
		if (guildPerms.SendMessages) { perms.Add("Send Messages"); }
		if (guildPerms.SendTTSMessages) { perms.Add("Send TTS Messages"); }
		if (guildPerms.Speak) { perms.Add("Speak"); }
		if (guildPerms.Stream) { perms.Add("Stream"); }
		if (guildPerms.UseApplicationCommands) { perms.Add("Use Application Commands"); }
		if (guildPerms.UseExternalEmojis) { perms.Add("Use External Emojis"); }
		if (guildPerms.UseExternalStickers) { perms.Add("Use External Stickers"); }
		if (guildPerms.ViewAuditLog) { perms.Add("View Audit Log"); }
		if (guildPerms.ViewChannel) { perms.Add("View Channel"); }
		if (guildPerms.ViewGuildInsights) { perms.Add("View Guild Insights"); }

		return perms.ToArray();
	}
}
