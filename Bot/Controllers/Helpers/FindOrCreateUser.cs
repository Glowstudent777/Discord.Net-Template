using Discord;
using Bot.Models;
using Microsoft.EntityFrameworkCore;

namespace Bot.Controllers.Helpers
{
    class FindOrCreateUser
    {
        public static async Task<User> Perform(IGuild guild, IGuildUser user, Database db)
        {
            var guildDb = await FindOrCreateGuild.Perform(guild, db);
            var userDb = await db.Users.FirstOrDefaultAsync(x => x.UserID == user.Id && x.GuildId == guild.Id);

            if (userDb != null)
            {
                return userDb;
            }
            else
            {
                var newUser = new User
                {
                    UserID = user.Id,
                    MessageCount = 0,
                    GuildId = guild.Id
                };

                db.Users.Add(newUser);
                //guildDb.Users.Add(newUser);
                await db.SaveChangesAsync();
                return newUser;
            }
        }
    }
}
