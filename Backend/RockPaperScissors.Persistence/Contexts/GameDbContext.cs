using Microsoft.EntityFrameworkCore;
using RockPaperScissors.Application.Services;
using RockPaperScissors.Domain.Entities;

namespace RockPaperScissors.Persistence;

public class GameDbContext : DbContext, IDbContext
{
    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Game> Games { get; set; } = null!;
    public DbSet<GameRound> GameRounds { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique(); 

        modelBuilder.Entity<Game>()
            .HasOne(g => g.Creator)
            .WithMany(u => u.Games)
            .HasForeignKey(g => g.CreatorId);

        modelBuilder.Entity<Game>()
            .HasOne(g => g.Opponent)
            .WithMany()
            .HasForeignKey(g => g.OpponentId)
            .IsRequired(false);
    }
}