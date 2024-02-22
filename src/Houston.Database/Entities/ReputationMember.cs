using Houston.Database.Entities.Base;

namespace Houston.Database.Entities;

public class ReputationMember : MemberEntity
{
	public long Reputation { get; set; }

	public ReputationMember(ulong guildId, ulong memberId) : base(guildId, memberId) { }
}
