namespace RockPaperScissors.Domain.Entities;

public class UserRating
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public int Rating { get; set; } = 0;
}