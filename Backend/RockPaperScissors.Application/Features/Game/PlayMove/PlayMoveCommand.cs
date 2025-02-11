using RockPaperScissors.Application.Abstractions;
using RockPaperScissors.Application.DTOs.Game.PlayMove;

namespace RockPaperScissors.Application.Features.Game.PlayMove;

public class PlayMoveCommand : ICommand<bool>
{
    public PlayMoveRequest RequestDto { get; set; }

    public PlayMoveCommand(PlayMoveRequest requestDto)
    {
        RequestDto = requestDto;
    }
}