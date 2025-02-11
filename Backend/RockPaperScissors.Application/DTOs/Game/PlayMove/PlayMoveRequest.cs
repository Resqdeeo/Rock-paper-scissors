namespace RockPaperScissors.Application.DTOs.Game.PlayMove;

public class PlayMoveRequest
{
    public Guid GameId { get; set; }
    public Guid PlayerId { get; set; }
    public string Move { get; set; } // Камень, Ножницы, Бумага
}