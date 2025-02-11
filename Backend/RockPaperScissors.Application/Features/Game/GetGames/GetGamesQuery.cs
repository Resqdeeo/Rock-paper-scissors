using RockPaperScissors.Application.Abstractions;
using RockPaperScissors.Application.DTOs.Game.GetGames;

namespace RockPaperScissors.Application.Features.Game.GetGames;

public class GetGamesQuery : IQuery<List<GetGamesResponse>>
{
    public int Page { get; }
    public int PageSize { get; }
    
    public GetGamesQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}