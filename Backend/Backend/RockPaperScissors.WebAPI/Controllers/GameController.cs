using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RockPaperScissors.Application.Abstractions;
using RockPaperScissors.Application.DTOs.Game.CreateGame;
using RockPaperScissors.Application.DTOs.Game.GetGames;
using RockPaperScissors.Application.DTOs.Game.GetRatingsQuery;
using RockPaperScissors.Application.Features.Game.CreateGame;
using RockPaperScissors.Application.Features.Game.GetGames;
using RockPaperScissors.Application.Features.Game.GetRatingsQuery;

namespace RockPaperScissors.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class GameController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public GameController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateGame([FromBody] CreateGameRequest request)
    {
        var command = new CreateGameCommand(request.CreatorId, request.MaxRating);
        try
        {
            var result = await _commandDispatcher.Send<CreateGameCommand, Guid>(command);
            return Ok(result); 
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    // Получение списка всех игр
    [HttpGet("games")]
    public async Task<IActionResult> GetGameList([FromBody] GetGamesRequest request)
    {
        try
        {
            var query = new GetGamesQuery(request.Page, request.Size);
            var games = await _queryDispatcher.Send<GetGamesQuery, List<GetGamesResponse>>(query);
            return Ok(games); // Возвращаем список игр
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("ratings")]
    public async Task<IActionResult> GetRatings()
    {
        var query = new GetRatingsQuery();
        try
        {
            var ratings = await _queryDispatcher.Send<GetRatingsQuery, List<GetRatingsResponse>>(query);
            return Ok(ratings); 
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}