namespace Bot.Models
{
    public class Guild
    {
        public ulong GuildId { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }

    public class User
    {
        public ulong UserID { get; set; }
        public int MessageCount { get; set; }

        public ulong GuildId { get; set; }
        public Guild Guild { get; set; }
    }
}