using RockPaperScissors.Application.Abstractions;
using RockPaperScissors.Application.DTOs.Game.GetRatingsQuery;
using RockPaperScissors.Domain.Repositories;

namespace RockPaperScissors.Application.Features.Game.GetRatingsQuery;

public class GetRatingsQueryHandler : IQueryHandler<GetRatingsQuery, List<GetRatingsResponse>>
{
    private readonly IRatingRepository _ratingRepository;

    public GetRatingsQueryHandler(IRatingRepository ratingRepository)
    {
        _ratingRepository = ratingRepository;
    }

    public async Task<List<GetRatingsResponse>> Handle(GetRatingsQuery query)
    {
        var ratings = await _ratingRepository.GetAllAsync();
        return ratings
            .Select(r => new GetRatingsResponse
            {
                Username = r.Username,
                Rating = r.Rating
            })
            .ToList();
    }
}