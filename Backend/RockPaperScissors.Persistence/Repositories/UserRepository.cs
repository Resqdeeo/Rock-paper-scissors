using Microsoft.EntityFrameworkCore;
using RockPaperScissors.Domain.Entities;
using RockPaperScissors.Domain.Repositories;

namespace RockPaperScissors.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly GameDbContext _context;

    public UserRepository(GameDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}