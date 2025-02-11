using RockPaperScissors.Domain.Repositories;
using RockPaperScissors.Persistence;
using RockPaperScissors.Persistence.Repositories;

namespace RockPaperScissors.WebAPI.Configurations;

public static class MongoConfiguration 
{
    public static IServiceCollection AddMongoDb(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["MongoSettings:ConnectionString"];
        var databaseName = configuration["MongoSettings:DatabaseName"];
        
        services.AddSingleton<MongoDbContext>(serviceProvider => 
            new MongoDbContext(connectionString, databaseName));
        
        
        services.AddScoped<IRatingRepository, RatingRepository>();

        return services;
    }
}