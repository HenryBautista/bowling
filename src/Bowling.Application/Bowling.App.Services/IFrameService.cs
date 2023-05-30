namespace Bowling.App.Services;

using Bowling.Entities;
using LanguageExt.Common;

public interface IFrameService
{
    Task<Result<Frame>> CreateFrameAsync(Frame frame);
    Task<IReadOnlyList<Frame>> GetAllAsync();
    Task<Result<Frame>> GetFrameAsync(int id);
    Task<IList<Frame>> GetFramesByGameAsync(int gameId);
    Task<Result<Frame>> UpdateFrameAsync(Frame frame);
    Task RemoveFrame(int id);
    Task<Frame> RollIntoFrameAsync(
        int gameId,
        int currentFrameNumber,
        char PinsDown);
}
