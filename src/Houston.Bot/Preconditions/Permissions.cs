using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Houston.Bot.Common;

namespace Houston.Bot.Preconditions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    class RequirePermissionsAttribute : Discord.Interactions.PreconditionAttribute
    {
        private readonly GuildPermission[] _permissions;

        public RequirePermissionsAttribute(params GuildPermission[] permissions)
        {
            _permissions = permissions;
        }

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

            var missingPermissions = _permissions
                .Where(permission => !user.GuildPermissions.Has(permission))
                .ToList();

            if (missingPermissions.Any() && !Config.Admins.Contains(user.Id.ToString()))
            {
                var missingPermissionNames = string.Join(", ", missingPermissions.Select(permission => permission.ToString()));
                return PreconditionResult.FromError($"You must have the following permissions to run this command: {missingPermissionNames}");
            }

            return PreconditionResult.FromSuccess();
        }
    }
}
