using Microsoft.EntityFrameworkCore;
using RockPaperScissors.Domain.Entities;
using RockPaperScissors.Domain.Repositories;

namespace RockPaperScissors.Persistence.Repositories;

public class GameRepository : IGameRepository
{
    private readonly GameDbContext _context;

    public GameRepository(GameDbContext context)
    {
        _context = context;
    }

    public async Task<Game?> GetByIdAsync(Guid id)
    {
        return await _context.Games.Include(g => g.Creator)
            .Include(g => g.Opponent)
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<IEnumerable<Game>> GetAvailableGamesAsync()
    {
        return await _context.Games
            .Where(g => g.Status == "waiting")
            .OrderByDescending(g => g.CreatedAt)
            .ToListAsync();
    }

    public async Task AddAsync(Game game)
    {
        await _context.Games.AddAsync(game);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Game game)
    {
        _context.Games.Update(game);
        await _context.SaveChangesAsync();
    }
    
    public async Task<List<Game>> GetPagedGamesAsync(int page, int pageSize)
    {
        return await _context.Games
            .Include(g => g.Creator) // Подгружаем данные о создателе игры
            .OrderBy(g => g.Status == "waiting" ? 0 : g.Status == "in_progress" ? 1 : 2) // Сначала waiting, затем in_progress, затем finished
            .ThenByDescending(g => g.CreatedAt) // Среди них сортируем по дате создания (новые выше)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}