namespace Bowling.Services;

using Bowling.Entities;

public interface IGameService
{
    Task<Game> CreateGameAsync(Game game);
}
