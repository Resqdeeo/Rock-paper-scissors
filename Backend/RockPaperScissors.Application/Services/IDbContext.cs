using Microsoft.EntityFrameworkCore;
using RockPaperScissors.Domain.Entities;

namespace RockPaperScissors.Application.Services;

public interface IDbContext
{
    public DbSet<User> Users { get; set; } 
    public DbSet<Game> Games { get; set; } 
    public DbSet<GameRound> GameRounds { get; set; } 
    
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    DbSet<T> Set<T>() where T : class;
}