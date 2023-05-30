namespace Bowling.App.Services;

using Bowling.App.Services.Dtos;

public interface IBowlingManagerService
{
    Task<RollResultDto> RollAsync(RollDto roll);
    Task<ScoreDto> GetScoreAsync(int gameId);
}
