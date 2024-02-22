namespace Houston.Database.Entities;

public class User
{
	public ulong UserId { get; internal set; }

	public User(ulong userId)
	{
		UserId = userId;
	}

	internal User() { }
}