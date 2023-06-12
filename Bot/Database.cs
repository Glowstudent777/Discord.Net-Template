using Bot.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Bot
{
    public class Database : DbContext
    {
        public Database(DbContextOptions<Database> options)
            : base(options) { }

        public DbSet<Guild> Guilds { get; set; }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<Database>
    {
        public Database CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Database>();
            var connectionString = Environment.GetEnvironmentVariable("DATABASE");
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 29)));

            return new Database(optionsBuilder.Options);
        }
    }
}
