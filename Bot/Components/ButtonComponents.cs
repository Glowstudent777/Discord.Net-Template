using Discord;

namespace Bot.Components
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
    }
}