using Discord;
using Bot.Models;
using Microsoft.EntityFrameworkCore;

namespace Bot.Controllers.Helpers
{
    class FindOrCreateGuild
    {
        public static async Task<Guild> Perform(IGuild guild, Database db)
        {
            var GuildDb = await db.Guilds.FirstOrDefaultAsync(x => x.GuildID == guild.Id.ToString());
            if (GuildDb == null)
            {
                GuildDb = new Guild
                {
                    GuildID = guild.Id.ToString(),
                    CacheDate = DateTime.Now
                };
                await db.Guilds.AddAsync(GuildDb);
                await db.SaveChangesAsync();
            }
            return GuildDb;
        }
    }
}
