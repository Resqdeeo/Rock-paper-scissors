using RockPaperScissors.Application.Abstractions;
using RockPaperScissors.Application.DTOs.Game.JoinGame;

namespace RockPaperScissors.Application.Features.Game.JoinGame;

public class JoinGameCommand : ICommand<bool>
{
    public JoinGameRequest RequestDto { get; set; }

    public JoinGameCommand(JoinGameRequest requestDto)
    {
        RequestDto = requestDto;
    }
}