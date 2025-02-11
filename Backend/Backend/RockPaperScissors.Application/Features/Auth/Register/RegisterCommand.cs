using RockPaperScissors.Application.Abstractions;
using RockPaperScissors.Application.DTOs.Auth.Register;

namespace RockPaperScissors.Application.Features.Auth.Register;

public class RegisterCommand : ICommand<RegisterResponse>
{
    public RegisterRequest RequestDto { get; set; }
    
    public RegisterCommand(RegisterRequest requestDto)
    {
        RequestDto = requestDto;
    }
}