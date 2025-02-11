using RockPaperScissors.Application.Abstractions;
using RockPaperScissors.Application.DTOs.Auth;
using RockPaperScissors.Application.Services;
using RockPaperScissors.Domain.Repositories;

namespace RockPaperScissors.Application.Features.Auth.Login;

public class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResponse>
{
    private readonly IJwtService _jwtService;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public LoginCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtService jwtService)
    {
        _jwtService = jwtService;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<LoginResponse> Handle(LoginCommand command)
    {
        var user = await _userRepository.GetByUsernameAsync(command.RequestDto.Username);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        // Проверка пароля
        if (!_passwordHasher.VerifyPassword(command.RequestDto.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        // Генерация JWT
        var token = _jwtService.GenerateToken(user.Id, command.RequestDto.Username);

        return new LoginResponse { Token = token };
    }
}