using Discord;
using Microsoft.EntityFrameworkCore;
using Bot.Models;


namespace Bot.Controllers.Helpers
{
    class FindOrCreateGuild
    {
        public static async Task<Guild> Perform(IGuild guild, Database db)
        {
            var guilddb = await db.Guilds.FirstOrDefaultAsync(x => x.GuildId == guild.Id);
            if (guilddb != null)
            {
                return guilddb;
            }
            else
            {
                Console.WriteLine($"Creating new guild {guild.Id}");
                var newGuild = new Guild
                {
                    GuildId = guild.Id,
                };
                await db.Guilds.AddAsync(newGuild);
                await db.SaveChangesAsync();
                return newGuild;
            }
        }
    }
}
