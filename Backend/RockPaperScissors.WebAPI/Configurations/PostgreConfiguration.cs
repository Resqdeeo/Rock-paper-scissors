using Microsoft.EntityFrameworkCore;
using RockPaperScissors.Domain.Repositories;
using RockPaperScissors.Persistence;
using RockPaperScissors.Persistence.Repositories;

namespace RockPaperScissors.WebAPI.Configurations;

public static class PostgreConfiguration
{
    public static IServiceCollection AddDatabase(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<GameDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PostgresConnection")));
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IGameRepository, GameRepository>();

        return services;
    }
}