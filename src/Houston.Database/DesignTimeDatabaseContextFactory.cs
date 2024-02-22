using System.IO;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Houston.Database;

public class DesignTimeDatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
	public DatabaseContext CreateDbContext(string[] args)
	{
		var configuration = new ConfigurationBuilder()
			.AddJsonFile(Directory.GetCurrentDirectory() + "/../Houston.Bot/appsettings.json")
			.Build();

		return new DatabaseContext(configuration);
	}
}