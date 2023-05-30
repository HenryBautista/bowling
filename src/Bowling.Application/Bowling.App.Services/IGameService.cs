namespace Bowling.App.Services;

using Bowling.Entities;
using LanguageExt.Common;

public interface IGameService
{
    Task<Result<Game>> CreateGameAsync(Game game);
    Task<IReadOnlyList<Game>> GetAllAsync();
    Task<Result<Game>> GetGameResultAsync(int id);
    Task<Game?> GetGameAsync(int id);
    Task<Game> UpdateGameAsync(Game game);
}
