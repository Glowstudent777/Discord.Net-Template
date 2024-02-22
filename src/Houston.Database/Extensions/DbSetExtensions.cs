using Microsoft.EntityFrameworkCore;
using Houston.Database.Entities.Base;
using Houston.Database.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Houston.Database.Extensions;

public static class DbSetExtensions
{
	public static Task<T> GetForGuidAsync<T>(this DbSet<T> dbSet, Guid id) where T : GuidEntity
	{
		return dbSet.FirstOrDefaultAsync(x => x.Id == id);
	}
	public static Task<T> GetForGuildAsync<T>(this DbSet<T> dbSet, ulong guildId) where T : GuildEntity
	{
		return dbSet.FirstOrDefaultAsync(x => x.GuildId == guildId);
	}
	public static Task<List<T>> GetAllForGuildAsync<T>(this DbSet<T> dbSet, ulong guildId) where T : GuildChannelEntity
	{
		return dbSet.Where(x => x.GuildId == guildId).ToListAsync();
	}
	public static Task<T> GetForGuildChannelAsync<T>(this DbSet<T> dbSet, ulong guildId, ulong channelId) where T : GuildChannelEntity
	{
		return dbSet.FirstOrDefaultAsync(x => x.GuildId == guildId && x.ChannelId == channelId);
	}
	public static Task<T> GetForMessageAsync<T>(this DbSet<T> dbSet, ulong messageId) where T : MessageEntity
	{
		return dbSet.FirstOrDefaultAsync(x => x.MessageId == messageId);
	}
	public static Task<T> GetForMemberAsync<T>(this DbSet<T> dbSet, ulong guildId, ulong memberId) where T : MemberEntity
	{
		return dbSet.FirstOrDefaultAsync(x => x.GuildId == guildId && x.MemberId == memberId);
	}
	public static Task<List<T>> GetForAllGuildMembersAsync<T>(this DbSet<T> dbSet, ulong guildId) where T : MemberEntity
	{
		return dbSet.Where(x => x.GuildId == guildId).ToListAsync();
	}
}
