using Discord.Interactions;

namespace Houston.Bot.Modules
{
	public abstract class InteractionsBase : InteractionModuleBase<ShardedInteractionContext>
	{
		protected Func<string, Discord.Embed, Task> Responder;
		protected Func<string, Discord.Embed, Task> PublicResponder;

		public InteractionsBase()
		{
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