namespace Bowling.Repositories.Sql;

using System.Threading.Tasks;
using Bowling.Entities;
using Bowling.Interfaces;

public class PlayerRepository : IPlayerRepository
{

    private readonly BowlingDbContext BowlingDbContext;

    public PlayerRepository(BowlingDbContext bowlingDbContext)
    {
        this.BowlingDbContext = bowlingDbContext;
    }

    public async Task<Player> AddAsync(Player player)
    {
        await this.BowlingDbContext.Players.AddAsync(player);
        await this.BowlingDbContext.SaveChangesAsync();
        
        return player;
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<Player>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Player?> GetByIdAsync(int id)
    {
        return await this.BowlingDbContext.Players.FindAsync(id);
    }

    public Task UpdateAsync(Player entity)
    {
        throw new NotImplementedException();
    }
}
