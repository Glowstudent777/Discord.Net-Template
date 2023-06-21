using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bot.Models
{
    public class Guild
    {
        public ulong GuildId { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }

    public class User
    {
        // Primary auto incremented Id
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public ulong UserID { get; set; }
        public int MessageCount { get; set; }

        public ulong GuildId { get; set; }
        public virtual Guild Guild { get; set; }
    }
}