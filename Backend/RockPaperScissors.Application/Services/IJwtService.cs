namespace RockPaperScissors.Application.Services;

public interface IJwtService
{
    string GenerateToken(Guid userId, string username);
    bool ValidateToken(string token);
    string GetUsernameFromToken(string token);
}