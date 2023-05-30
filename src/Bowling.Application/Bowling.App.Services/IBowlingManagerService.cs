namespace Bowling.App.Services;

using Bowling.App.Services.Dtos;

public interface IBowlingManagerService
{
    Task<RollResultDto> Roll(RollDto roll);
    Task<object> GetScore(int gameId);
}
