using RockPaperScissors.Application.Abstractions;
using RockPaperScissors.Domain.Repositories;

namespace RockPaperScissors.Application.Features.Game.JoinGame;

public class JoinGameCommandHandler : ICommandHandler<JoinGameCommand, bool>
{
    private readonly IGameRepository _gameRepository;
    private readonly IUserRepository _userRepository;

    public JoinGameCommandHandler(IGameRepository gameRepository, IUserRepository userRepository)
    {
        _gameRepository = gameRepository;
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(JoinGameCommand command)
    {
        // Проверяем, существует ли игра
        var game = await _gameRepository.GetByIdAsync(command.RequestDto.GameId);
        if (game == null || game.Status != "waiting")
        {
            return false; // Игра не существует или уже началась
        }

        // Проверяем, существует ли игрок
        var player = await _userRepository.GetByIdAsync(command.RequestDto.PlayerId);
        if (player == null || player.Rating > game.MaxRating)
        {
            return false; // Игрок не существует или его рейтинг слишком высок
        }

        // Присоединяем игрока к игре
        game.OpponentId = command.RequestDto.PlayerId;
        game.Status = "in_progress"; // Статус игры изменяется на "in_progress"

        // Сохраняем изменения в репозитории
        await _gameRepository.UpdateAsync(game);

        return true; // Игрок успешно присоединился
    }
}