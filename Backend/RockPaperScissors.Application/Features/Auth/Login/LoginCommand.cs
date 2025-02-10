using RockPaperScissors.Application.Abstractions;
using RockPaperScissors.Application.DTOs.Auth;

namespace RockPaperScissors.Application.Features.Auth.Login;

public class LoginCommand : ICommand<LoginResponse>
{
    public LoginRequest RequestDto { get; set; }

    public LoginCommand(LoginRequest requestDto)
    {
        RequestDto = requestDto;
    }
}