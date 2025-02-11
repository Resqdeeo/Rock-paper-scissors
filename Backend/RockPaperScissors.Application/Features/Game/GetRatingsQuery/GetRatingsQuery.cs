using RockPaperScissors.Application.Abstractions;
using RockPaperScissors.Application.DTOs.Game.GetRatingsQuery;

namespace RockPaperScissors.Application.Features.Game.GetRatingsQuery;

public class GetRatingsQuery : IQuery<List<GetRatingsResponse>> { }