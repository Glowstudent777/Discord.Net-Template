using Discord;

namespace Houston.Bot.Components
{
	public class ButtonComponents
	{
		public static ButtonComponent ToggleButton(string Label, string CustomId, bool IsDisabled = false, ButtonStyle Style = ButtonStyle.Success)
		{
			return new ButtonBuilder()
				.WithLabel(Label)
				.WithCustomId("togglebutton:" + CustomId)
				.WithStyle(Style)
				.WithDisabled(IsDisabled)
				.Build();
		}

		public static ButtonComponent TriButton(string Label, string CustomId, bool IsDisabled = false, ButtonStyle Style = ButtonStyle.Success)
		{
			return new ButtonBuilder()
				.WithLabel(Label)
				.WithCustomId("tributton:" + CustomId)
				.WithStyle(Style)
				.WithDisabled(IsDisabled)
				.Build();
		}

		public static ButtonComponent LinkButton(string Label, string URL = "https://example.com/", bool IsDisabled = false)
		{
			return new ButtonBuilder()
				.WithLabel(Label)
				.WithStyle(ButtonStyle.Link)
				.WithUrl(URL)
				.WithDisabled(IsDisabled)
				.Build();
		}

		public static ButtonComponent SaveButton(string Label, string CustomId, bool IsDisabled = false)
		{
			return new ButtonBuilder()
				.WithLabel(Label)
				.WithCustomId("savebutton:" + CustomId)
				.WithStyle(ButtonStyle.Primary)
				.WithDisabled(IsDisabled)
				.Build();
		}
	}
}