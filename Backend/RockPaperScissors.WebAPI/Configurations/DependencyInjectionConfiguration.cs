using RockPaperScissors.Application.Abstractions;
using RockPaperScissors.Application.DTOs.Auth;
using RockPaperScissors.Application.DTOs.Auth.Register;
using RockPaperScissors.Application.DTOs.Game.GetGames;
using RockPaperScissors.Application.DTOs.Game.GetRatingsQuery;
using RockPaperScissors.Application.Features.Auth.Login;
using RockPaperScissors.Application.Features.Auth.Register;
using RockPaperScissors.Application.Features.Game.CreateGame;
using RockPaperScissors.Application.Features.Game.GetGames;
using RockPaperScissors.Application.Features.Game.GetRatingsQuery;
using RockPaperScissors.Infrastructure.CQRS;
using RockPaperScissors.Application.Services;
using RockPaperScissors.Infrastructure.Services;

namespace RockPaperScissors.WebAPI.Configurations;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddApplicationServices(IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ICommandDispatcher, CommandDispatcher>();

        services.AddScoped<ICommandHandler<LoginCommand, LoginResponse>, LoginCommandHandler>();
        services.AddScoped<ICommandHandler<RegisterCommand, RegisterResponse>, RegisterCommandHandler>();

        services.AddScoped<ICommandHandler<CreateGameCommand, Guid>, CreateGameCommandHandler>();
        services.AddScoped<IQueryHandler<GetGamesQuery, List<GetGamesResponse>>, GetGamesQueryHandler>();
        services.AddScoped<IQueryHandler<GetRatingsQuery, List<GetRatingsResponse>>, GetRatingsQueryHandler>();
        return services;
    }
}