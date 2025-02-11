namespace RockPaperScissors.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty; 
    public int Rating { get; set; } = 0; // Начальный рейтинг
    
    public ICollection<Game> Games { get; set; } = new List<Game>();
}