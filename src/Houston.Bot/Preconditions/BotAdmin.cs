using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Houston.Bot.Common;


namespace Houston.Bot.Preconditions
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public class RequireBotAdminAttribute : PreconditionAttribute
	{
		public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo commandInfo, IServiceProvider services)
		{
			if (context.Guild == null)
			{
				return Task.FromResult(PreconditionResult.FromError("This command can only be executed from within server channels."));
			}

			var user = context.User as SocketGuildUser;

			if (user == null)
			{
				return Task.FromResult(PreconditionResult.FromError("This command can only be executed in a server."));
			}

			if (!Config.Admins.Contains(user.Id.ToString()))
			{
				return Task.FromResult(PreconditionResult.FromError($"You must have permission to run this command"));
			}

			return Task.FromResult(PreconditionResult.FromSuccess());
		}
	}
}