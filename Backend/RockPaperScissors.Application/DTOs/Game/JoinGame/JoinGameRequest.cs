namespace RockPaperScissors.Application.DTOs.Game.JoinGame;

public class JoinGameRequest
{
    public Guid GameId { get; set; }
    public Guid PlayerId { get; set; }
    public int PlayerRating { get; set; }
}