using RockPaperScissors.Application.Abstractions;
using RockPaperScissors.Application.DTOs.Game.CreateGame;

namespace RockPaperScissors.Application.Features.Game.CreateGame;

public class CreateGameCommand : ICommand<Guid>
{
    public Guid CreatorId { get; }
    public int MaxRating { get; }

    public CreateGameCommand(Guid creatorId, int maxRating)
    {
        CreatorId = creatorId;
        MaxRating = maxRating;
    }
}