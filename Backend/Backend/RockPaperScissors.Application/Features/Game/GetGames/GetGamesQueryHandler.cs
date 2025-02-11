using RockPaperScissors.Application.Abstractions;
using RockPaperScissors.Application.DTOs.Game.GetGames;
using RockPaperScissors.Domain.Repositories;

namespace RockPaperScissors.Application.Features.Game.GetGames;

public class GetGamesQueryHandler : IQueryHandler<GetGamesQuery, List<GetGamesResponse>>
{
    private readonly IGameRepository _gameRepository;

    public GetGamesQueryHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<List<GetGamesResponse>> Handle(GetGamesQuery query)
    {
        var games = await _gameRepository.GetPagedGamesAsync(query.Page, query.PageSize);

        return games
            .OrderByDescending(g => g.CreatedAt)
            .ThenBy(g => g.Status == "waiting" ? 0 : 1) // Сначала новые и ожидающие
            .Select(g => new GetGamesResponse(g))
            .ToList();
    }
}