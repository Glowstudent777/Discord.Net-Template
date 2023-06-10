using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Bot
{
    public class Database : DbContext
    {
        public DbSet<Models.Guild> Guilds { get; set; }

        public Database(DbContextOptions<Database> options) : base(options) { }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<Database>
    {
        public Database CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<Database>();
            var connectionString = Environment.GetEnvironmentVariable("DATABASE");
            builder.UseSqlServer($@"{connectionString}");

            return new Database(builder.Options);
        }
    }
}
