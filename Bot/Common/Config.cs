using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace Bot.Common
{
    public static class Config
    {
        public static string EmbedSpacer { get; set; } = "https://cdn.animeinterlink.com/r/embed_spacer.png";

        public static Colors Colors { get; set; } = new Colors();
    }

    public class Colors
    {
        public Discord.Color Primary { get; set; } = Convert.ToUInt32("0x0099FF", 16);
        public Discord.Color Success { get; set; } = Convert.ToUInt32("0x3091BF", 16);
        public Discord.Color Warning { get; set; } = Convert.ToUInt32("0xfee75c", 16);
        public Discord.Color Error { get; set; } = Convert.ToUInt32("0xed4245", 16);
        public Discord.Color Invisible { get; set; } = Convert.ToUInt32("0x2f3136", 16);
    }
}