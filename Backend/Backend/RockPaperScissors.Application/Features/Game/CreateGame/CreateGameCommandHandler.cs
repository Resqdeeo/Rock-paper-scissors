using RockPaperScissors.Application.Abstractions;
using RockPaperScissors.Domain.Repositories;

namespace RockPaperScissors.Application.Features.Game.CreateGame;

public class CreateGameCommandHandler : ICommandHandler<CreateGameCommand, Guid>
{
    private readonly IGameRepository _gameRepository;
    private readonly IUserRepository _userRepository;

    public CreateGameCommandHandler(IGameRepository gameRepository, IUserRepository userRepository)
    {
        _gameRepository = gameRepository;
        _userRepository = userRepository;
    }

    public async Task<Guid> Handle(CreateGameCommand command)
    {
        var user = await _userRepository.GetByIdAsync(command.CreatorId);
        if (user == null)
            throw new Exception("User not found");

        var newGame = new Domain.Entities.Game
        {
            CreatorId = command.CreatorId,
            Creator = user,
            MaxRating = command.MaxRating
        };

        await _gameRepository.AddAsync(newGame);
        return newGame.Id;
    }
}