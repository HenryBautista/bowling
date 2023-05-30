namespace Bowling.App.Services;

using Bowling.Entities;
using LanguageExt.Common;

public interface IPlayerService
{
    Task<Result<Player>> CreatePlayerAsync(Player player);
    Task<Result<Player>> GetPlayerAsync(int id);
}
