namespace RockPaperScissors.Domain.Entities;

public class Game
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Status { get; set; } = "waiting"; // waiting, in_progress, finished
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int MaxRating { get; set; } // Ограничение рейтинга

    public Guid CreatorId { get; set; } // ID создателя игры
    public User Creator { get; set; } = null!;  

    public Guid? OpponentId { get; set; } // ID противника
    public User? Opponent { get; set; }

    public ICollection<GameRound> Rounds { get; set; } = new List<GameRound>();
}