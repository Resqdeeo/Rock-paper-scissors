namespace RockPaperScissors.Application.Services;

public interface IDbSeeder
{
    public Task SeedAsync(IDbContext context);
}