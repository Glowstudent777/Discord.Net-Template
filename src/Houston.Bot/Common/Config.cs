namespace Houston.Bot.Common
{
	public static class Config
	{
		public static string EmbedSpacer { get; set; } = "https://cdn.animeinterlink.com/r/embed_spacer.png";

		public static Colors Colors { get; set; } = new Colors();

		public static string SupportServer { get; set; } = "https://discord.gg/A6AvhysUw9";

		public static string Version { get; set; } = "2024.23.01";
		// Array or list of strings
		public static string[] ReleaseNotes { get; set; } = new string[]
		{
			"Initial Release"
		};

		public static string[] Admins { get; } = new string[] { "557691883518951435" };
	}

	public class Colors
	{
		public Discord.Color Primary { get; set; } = Convert.ToUInt32("0x0099FF", 16);
		public Discord.Color Success { get; set; } = Convert.ToUInt32("0x57f287", 16);
		public Discord.Color Warning { get; set; } = Convert.ToUInt32("0xfee75c", 16);
		public Discord.Color Error { get; set; } = Convert.ToUInt32("0xed4245", 16);
		public Discord.Color Invisible { get; set; } = Convert.ToUInt32("0x2b2d31", 16);
	}
}