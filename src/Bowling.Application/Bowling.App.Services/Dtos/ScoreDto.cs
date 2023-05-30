namespace Bowling.App.Services.Dtos;

public class ScoreDto
{
    public int GameId { get; set; }
    public string? PlayerName { get; set; }
    public int TotalScore { get; set; }
}