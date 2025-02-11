using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using RockPaperScissors.Application.Abstractions;
using RockPaperScissors.Application.DTOs.Game.CreateGame;
using RockPaperScissors.Application.DTOs.Game.GetGames;
using RockPaperScissors.Application.DTOs.Game.GetRatingsQuery;
using RockPaperScissors.Application.DTOs.Game.JoinGame;
using RockPaperScissors.Application.DTOs.Game.PlayMove;
using RockPaperScissors.Application.Features.Game.CreateGame;
using RockPaperScissors.Application.Features.Game.GetGames;
using RockPaperScissors.Application.Features.Game.GetRatingsQuery;
using RockPaperScissors.Application.Features.Game.JoinGame;
using RockPaperScissors.Application.Features.Game.PlayMove;

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
        var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value; 
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized("Invalid token");
        
        var command = new CreateGameCommand(userId, request.MaxRating);
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
        var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized("Invalid token");
        
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
    
    [HttpPost("join")]
    public async Task<IActionResult> JoinGame([FromBody] JoinGameRequest request)
    {
        var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized("Invalid token");

        // Создаем команду для присоединения к игре
        var command = new JoinGameCommand(new JoinGameRequest
        {
            GameId = request.GameId,
            PlayerId = userId // ID игрока берём из токена
        });

        try
        {
            var result = await _commandDispatcher.Send<JoinGameCommand, bool>(command);
            return result ? Ok("Player joined the game.") : BadRequest("Unable to join the game.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("play")]
    public async Task<IActionResult> PlayMove([FromBody] PlayMoveRequest request)
    {
        var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized("Invalid token");

        // Создаем команду для выполнения хода
        var command = new PlayMoveCommand(new PlayMoveRequest
        {
            GameId = request.GameId,
            PlayerId = userId, // ID игрока берём из токена
            Move = request.Move
        });

        try
        {
            var result = await _commandDispatcher.Send<PlayMoveCommand, bool>(command);
            return result ? Ok("Move registered.") : BadRequest("Invalid move or game state.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}