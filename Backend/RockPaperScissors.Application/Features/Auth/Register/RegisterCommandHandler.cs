using RockPaperScissors.Application.Abstractions;
using RockPaperScissors.Application.DTOs.Auth.Register;
using RockPaperScissors.Application.Services;
using RockPaperScissors.Domain.Entities;
using RockPaperScissors.Domain.Repositories;

namespace RockPaperScissors.Application.Features.Auth.Register;

public class RegisterCommandHandler : ICommandHandler<RegisterCommand, RegisterResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<RegisterResponse> Handle(RegisterCommand command)
    {
        var existingUser = await _userRepository.GetByUsernameAsync(command.RequestDto.Username);
        if (existingUser != null)
        {
            throw new InvalidOperationException("Username already exists.");
        }
        
        var hashedPassword = _passwordHasher.HashPassword(command.RequestDto.Password);
        
        var newUser = new User
        {
            Id = Guid.NewGuid(),
            Username = command.RequestDto.Username,
            PasswordHash = hashedPassword
        };

        await _userRepository.AddAsync(newUser);

        return new RegisterResponse
        {
            Message = "Registration successful"
        };
    }
}