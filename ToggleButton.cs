using Discord;

namespace ComponentLib
{
    public class ToggleButton
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
    }
}