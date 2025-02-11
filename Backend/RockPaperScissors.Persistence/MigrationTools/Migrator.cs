using Microsoft.EntityFrameworkCore;
using RockPaperScissors.Application.Services;

namespace RockPaperScissors.Persistence.MigrationTools;

public class Migrator
{
    private readonly GameDbContext _context;
    private readonly IDbSeeder _seeder;

    public Migrator(GameDbContext context, IDbSeeder seeder)
    {
        _context = context;
        _seeder = seeder;
    }
    
    public async Task MigrateAsync()
    {
        try
        {
            await _context.Database.MigrateAsync().ConfigureAwait(false);
            await _seeder.SeedAsync(_context);
        }
        catch (Exception e)
        {
            Console.WriteLine($"migrations apply failed {e.Message}");
            throw;
        }
    }
}