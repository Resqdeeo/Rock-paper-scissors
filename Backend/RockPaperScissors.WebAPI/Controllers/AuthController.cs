using Microsoft.AspNetCore.Mvc;
using RockPaperScissors.Application.Abstractions;
using RockPaperScissors.Application.DTOs.Auth;
using RockPaperScissors.Application.DTOs.Auth.Register;
using RockPaperScissors.Application.Features.Auth.Login;
using RockPaperScissors.Application.Features.Auth.Register;

namespace RockPaperScissors.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public AuthController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }
    
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var command = new LoginCommand(loginRequest);
        try
        {
            var response = await _commandDispatcher.Send<LoginCommand, LoginResponse>(command);
            return Ok(response);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Invalid username or password.");
        }
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        var command = new RegisterCommand(registerRequest);
        try
        {
            var response = await _commandDispatcher.Send<RegisterCommand, RegisterResponse>(command);
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}