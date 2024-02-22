using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Houston.Database.Entities;
using Houston.Database.Entities.Base;

namespace Houston.Database.Extensions;

internal static class ModelBuilderExtensions
{
	public static void ConfigureGuildEntities(this ModelBuilder modelBuilder)
	{
		var types = Assembly.GetExecutingAssembly().GetTypes()
			.Where(x => x.IsAssignableTo(typeof(GuildEntity)) && !x.IsEquivalentTo(typeof(GuildEntity)));

		foreach (var type in types)
		{
			modelBuilder.Entity(type).HasKey("GuildId");
			modelBuilder.Entity(type).Property("GuildId").ValueGeneratedNever();
		}
	}

	public static void ConfigureMemberEntities(this ModelBuilder modelBuilder)
	{
		var types = Assembly.GetExecutingAssembly().GetTypes()
			.Where(x => x.IsAssignableTo(typeof(MemberEntity)) && !x.IsEquivalentTo(typeof(MemberEntity)));

		foreach (var type in types)
		{
			modelBuilder.Entity(type).HasKey("GuildId", "MemberId");
			modelBuilder.Entity(type).Property("GuildId").ValueGeneratedNever();
			modelBuilder.Entity(type).Property("MemberId").ValueGeneratedNever();
		}
	}

	public static void ConfigureOtherEntities(this ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<User>().HasKey(e => e.UserId);
		modelBuilder.Entity<User>().Property(e => e.UserId).ValueGeneratedNever();
	}
}
