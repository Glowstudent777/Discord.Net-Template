using Discord;
using Discord.Interactions;
using Houston.Bot.Modules;
using Houston.Database;

namespace Welcomer.Bot.Modules.Buttons;

public class StyleCycler
{
	public class CyclerGroup : InteractionsBase
	{
		private readonly DatabaseContext _database;

		public CyclerGroup(DatabaseContext database)
		{
			_database = database;
		}

		[ComponentInteraction("togglebutton:*")]
		[RequireUserPermission(GuildPermission.ManageGuild, Group = "user")]
		public async Task Toggle(string customId)
		{
			var interaction = Context.Interaction as IComponentInteraction;
			var message = interaction.Message as IUserMessage;

			if (Context.User.Id.ToString() == message.Interaction.User.Id.ToString())
			{
				var builder = new ComponentBuilder();
				var rows = ComponentBuilder.FromMessage(interaction.Message).ActionRows;

				for (int i = 0; i < rows.Count; i++)
				{
					foreach (var x in rows[i].Components)
					{
						switch (x)
						{
							case ButtonComponent button:
								var buttonid = button.CustomId.Split(':')[1];
								if (buttonid == customId)
								{
									builder.WithButton(button.ToBuilder().WithStyle(button.Style == ButtonStyle.Success ? ButtonStyle.Danger : ButtonStyle.Success), i);
								}
								else
									builder.WithButton(button.ToBuilder(), i);
								break;
							case SelectMenuComponent select:
								builder.WithSelectMenu(select.ToBuilder(), i);
								break;
							default:
								Console.WriteLine($"Unknown Component");
								break;
						}
					}
				}

				await interaction.UpdateAsync(x => x.Components = builder.Build());
			}
			else
				await RespondAsync("This button isn't for you!", ephemeral: true);
		}

		[ComponentInteraction("tributton:*")]
		[RequireUserPermission(GuildPermission.ManageGuild, Group = "user")]
		public async Task TriToggle(string customId)
		{
			var interaction = Context.Interaction as IComponentInteraction;
			var message = interaction.Message as IUserMessage;

			if (Context.User.Id.ToString() == message.Interaction.User.Id.ToString())
			{
				var builder = new ComponentBuilder();
				var rows = ComponentBuilder.FromMessage(interaction.Message).ActionRows;

				for (int i = 0; i < rows.Count; i++)
				{
					foreach (var x in rows[i].Components)
					{
						switch (x)
						{
							case ButtonComponent button:
								var buttonid = button.CustomId.Split(':')[1];
								if (buttonid == customId)
								{
									// Button progession is Success -> Primary -> Danger
									if (button.Style == ButtonStyle.Success)
										builder.WithButton(button.ToBuilder().WithStyle(ButtonStyle.Primary), i);
									else if (button.Style == ButtonStyle.Primary)
										builder.WithButton(button.ToBuilder().WithStyle(ButtonStyle.Danger), i);
									else if (button.Style == ButtonStyle.Danger)
										builder.WithButton(button.ToBuilder().WithStyle(ButtonStyle.Success), i);
								}
								else
									builder.WithButton(button.ToBuilder(), i);
								break;
							case SelectMenuComponent select:
								builder.WithSelectMenu(select.ToBuilder(), i);
								break;
							default:
								Console.WriteLine($"Unknown Component");
								break;
						}
					}
				}

				await interaction.UpdateAsync(x => x.Components = builder.Build());
			}
			else
				await RespondAsync("This button isn't for you!", ephemeral: true);
		}
	}
}