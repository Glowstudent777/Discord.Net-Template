using Discord.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Modules
{
    public abstract class InteractionsBase : InteractionModuleBase<ShardedInteractionContext>
    {
        protected Database _database;
        protected Func<string, Discord.Embed, Task> Responder;
        protected Func<string, Discord.Embed, Task> PublicResponder;

        public InteractionsBase(Database database)
        {
            _database = database;
            Responder = (response, embed) =>
            {
                if (embed != null)
                {
                    return RespondAsync(response, new[] { embed }, ephemeral: true);
                }
                return RespondAsync(response, ephemeral: true);
            };

            PublicResponder = (response, embed) =>
            {
                if (embed != null)
                {
                    return RespondAsync(response, new[] { embed }, ephemeral: false);
                }
                return RespondAsync(response, ephemeral: false);
            };
        }
    }
}
