using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Houston.Database.Entities;
using Houston.Database.Extensions;
using System;

namespace Houston.Database;

public class DatabaseContext : DbContext
{
	public DbSet<ReputationMember> ReputationMembers { get; internal set; }
	public DbSet<User> Users { get; internal set; }

	private readonly string _connectionString;

	public DatabaseContext(IConfiguration configuration)
	{
		_connectionString = Environment.GetEnvironmentVariable("DATABASE");
	}

	protected override void OnConfiguring(DbContextOptionsBuilder options)
	{
		options.UseNpgsql(_connectionString);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ConfigureGuildEntities();
		modelBuilder.ConfigureMemberEntities();
		modelBuilder.ConfigureOtherEntities();
	}
}
