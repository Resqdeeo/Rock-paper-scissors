namespace RockPaperScissors.Application.DTOs.Game.GetRatingsQuery;

public class GetRatingsResponse 
{
    public string Username { get; set; } = string.Empty;
    public int Rating { get; set; }
}