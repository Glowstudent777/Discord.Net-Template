using Discord;
using Microsoft.EntityFrameworkCore;
using Bot.Models;


namespace Bot.Controllers.Helpers
{
    class FindOrCreateGuild
    {
        public static async Task<Guild> Perform(IGuild guild, Database db)
        {
            var guilddb = await db.Guilds.FirstOrDefaultAsync(x => x.GuildID == guild.Id.ToString());
            if (guilddb == null)
            {
                guilddb = new Guild
                {
                    GuildID = guild.Id.ToString(),
                    CacheDate = DateTime.Now
                };
                await db.Guilds.AddAsync(guilddb);
                await db.SaveChangesAsync();
            }
            return guilddb;
        }
    }
}
