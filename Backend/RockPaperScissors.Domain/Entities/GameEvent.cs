namespace RockPaperScissors.Domain.Entities;

public class GameEvent
{
    public Guid GameId { get; set; }
    public string Action { get; set; } // Например, "Player1 Move", "Player2 Move", "Game End"
    public string Description { get; set; }
}