namespace Bowling.Services;

using Bowling.Entities;

public interface IGameService
{
    Task<Game> CreateGameAsync(Game game);
    Task<IReadOnlyList<Game>> GetAllAsync();
    Task<Game> GetGame(int id);
}
