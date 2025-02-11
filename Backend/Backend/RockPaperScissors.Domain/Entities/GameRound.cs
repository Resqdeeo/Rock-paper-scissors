namespace RockPaperScissors.Domain.Entities;

public class GameRound
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid GameId { get; set; }
    public Game Game { get; set; } = null!;

    public string Player1Move { get; set; } = string.Empty; // rock, paper, scissors
    public string Player2Move { get; set; } = string.Empty;

    public string Result { get; set; } = "pending"; // pending, win, lose, draw
}