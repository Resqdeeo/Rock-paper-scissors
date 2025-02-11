namespace RockPaperScissors.Application.DTOs.Game.GetGames;

public class GetGamesResponse
{
    public Guid Id { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public int MaxRating { get; set; }
    public string CreatorUsername { get; set; } = string.Empty;

    public GetGamesResponse(Domain.Entities.Game game)
    {
        Id = game.Id;
        Status = game.Status;
        CreatedAt = game.CreatedAt;
        MaxRating = game.MaxRating;
        CreatorUsername = game.Creator.Username;
    }
}