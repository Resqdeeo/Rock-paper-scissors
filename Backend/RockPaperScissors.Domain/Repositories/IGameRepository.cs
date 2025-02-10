using RockPaperScissors.Domain.Entities;

namespace RockPaperScissors.Domain.Repositories;

public interface IGameRepository
{
    Task<Game?> GetByIdAsync(Guid id);
    Task<IEnumerable<Game>> GetAvailableGamesAsync();
    Task AddAsync(Game game);
    Task UpdateAsync(Game game);
}