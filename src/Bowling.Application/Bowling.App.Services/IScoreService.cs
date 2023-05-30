namespace Bowling.App.Services;

using Bowling.App.Services.Dtos;

public interface IScoreService
{
    Task<ScoreDto> GetScoreAsync(int gameId);
}