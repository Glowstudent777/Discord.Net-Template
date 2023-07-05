using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace Bot.Preconditions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    class GuildOwner : Discord.Interactions.PreconditionAttribute

    {

        public async override Task<PreconditionResult> CheckRequirementsAsync(IInteractionContext context, Discord.Interactions.ICommandInfo commandInfo, IServiceProvider services)
        {
            if (context.Guild == null)
            {
                return PreconditionResult.FromError("This command can only be executed from within server channels.");
            }

            var user = context.User as SocketGuildUser;

            if (user == null)
            {
                return PreconditionResult.FromError("This command can only be executed in a server.");
            }

            var IsOwner = user.Guild.Owner.Id == user.Id;

            if (!IsOwner)
            {
                return PreconditionResult.FromError($"This command can only be executed by the guild owner ({user.Guild.Owner.Username}).");
            }

            return PreconditionResult.FromSuccess();
        }
    }
}
