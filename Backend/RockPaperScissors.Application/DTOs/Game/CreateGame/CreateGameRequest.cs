namespace RockPaperScissors.Application.DTOs.Game.CreateGame;

public class CreateGameRequest
{
    public Guid CreatorId { get; set; }
    public int MaxRating { get; set; }
}