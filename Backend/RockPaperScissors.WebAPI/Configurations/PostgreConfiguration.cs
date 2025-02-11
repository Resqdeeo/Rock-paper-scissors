using Microsoft.EntityFrameworkCore;
using RockPaperScissors.Application.Services;
using RockPaperScissors.Domain.Repositories;
using RockPaperScissors.Persistence;
using RockPaperScissors.Persistence.DataSeed;
using RockPaperScissors.Persistence.MigrationTools;
using RockPaperScissors.Persistence.Repositories;

namespace RockPaperScissors.WebAPI.Configurations;

public static class PostgreConfiguration
{
    public static IServiceCollection AddDatabase(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<GameDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PostgresConnection")));
        
        services.AddScoped<IDbSeeder, DbSeeder>();
        services.AddScoped<IDbContext, GameDbContext>();
        services.AddTransient<Migrator>();
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IGameRepository, GameRepository>();

        return services;
    }
}