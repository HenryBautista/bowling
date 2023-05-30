namespace Bowling.Interfaces;

using Bowling.Entities;

public interface IFrameRepository : IRepository<Frame>
{
    Task<Frame?> GetFrameByGameAndNumberAsync(
        int gameId,
        int number);
    Task<IList<Frame>> GetFramesByGameAsync(int gameId);
}
