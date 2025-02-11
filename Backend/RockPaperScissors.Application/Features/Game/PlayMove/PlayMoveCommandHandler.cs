using RockPaperScissors.Application.Abstractions;
using RockPaperScissors.Domain.Repositories;

namespace RockPaperScissors.Application.Features.Game.PlayMove;

public class PlayMoveCommandHandler : ICommandHandler<PlayMoveCommand, bool>
{
    private readonly IGameRepository _gameRepository;

    public PlayMoveCommandHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<bool> Handle(PlayMoveCommand command)
    {
        var game = await _gameRepository.GetByIdAsync(command.RequestDto.GameId);
        if (game == null || game.Status != "in_progress")
        {
            return false; // Если игра не в процессе
        }

        if (game.CreatorId == command.RequestDto.PlayerId)
        {
            game.CreatorMove = command.RequestDto.Move; // Сохраняем ход создателя
        }
        else if (game.OpponentId == command.RequestDto.PlayerId)
        {
            game.OpponentMove = command.RequestDto.Move; // Сохраняем ход противника
        }
        else
        {
            return false; // Игрок не найден в игре
        }

        // Если оба игрока сделали ход
        if (!string.IsNullOrEmpty(game.CreatorMove) && !string.IsNullOrEmpty(game.OpponentMove))
        {
            // Вычисление победителя
            var result = DetermineWinner(game);
            await HandleGameOver(game, result);
        }

        await _gameRepository.UpdateAsync(game);
        return true;
    }

    private string DetermineWinner(Domain.Entities.Game game)
    {
        // Логика определения победителя
        if (game.CreatorMove == game.OpponentMove)
        {
            return "draw"; // Ничья
        }

        if ((game.CreatorMove == "rock" && game.OpponentMove == "scissors") ||
            (game.CreatorMove == "scissors" && game.OpponentMove == "paper") ||
            (game.CreatorMove == "paper" && game.OpponentMove == "rock"))
        {
            return "creator"; // Победа создателя
        }
        return "opponent"; // Победа противника
    }

    private async Task HandleGameOver(Domain.Entities.Game game, string winner)
    {
        if (winner == "creator")
        {
            game.Creator.Rating += 3;
            game.Opponent.Rating -= 1;
        }
        else if (winner == "opponent")
        {
            game.Creator.Rating -= 1;
            game.Opponent.Rating += 3;
        }

        game.Status = "finished"; // Завершаем игру
        game.IsGameOver = true;
        await _gameRepository.UpdateAsync(game);
    }
}