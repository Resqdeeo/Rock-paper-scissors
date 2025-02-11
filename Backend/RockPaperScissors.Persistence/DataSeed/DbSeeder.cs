using Microsoft.EntityFrameworkCore;
using RockPaperScissors.Application.Services;
using RockPaperScissors.Domain.Entities;

namespace RockPaperScissors.Persistence.DataSeed;

public class DbSeeder : IDbSeeder
{
    private readonly IPasswordHasher _passwordHasher;

    public DbSeeder(IPasswordHasher passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }
    
    public async Task SeedAsync(IDbContext context)
    {
        if (await context.Users.AnyAsync())
        {
            return; 
        }

        var users = new List<User>
        {
            new()
            {
                Username = "Test",
                PasswordHash = _passwordHasher.HashPassword("Test"),
                Rating = 0
            },
            new ()
            {
                Username = "M9s0",
                PasswordHash = _passwordHasher.HashPassword("M9s0"),
                Rating = 0
            },
            new ()
            {
                Username = "Mr Kriper",
                PasswordHash = _passwordHasher.HashPassword("Mesi"),
                Rating = 0
            }
        };

        await context.Users.AddRangeAsync(users);
        await context.SaveChangesAsync();
    }
}