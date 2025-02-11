namespace RockPaperScissors.Application.Services;

public interface IJwtService
{
    string GenerateToken(string username);
    bool ValidateToken(string token);
    string GetUsernameFromToken(string token);
}