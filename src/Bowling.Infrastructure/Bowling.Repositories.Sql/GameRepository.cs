namespace Bowling.Repositories.Sql;

using System.Threading.Tasks;
using Bowling.Entities;
using Bowling.Interfaces;
using Microsoft.EntityFrameworkCore;

public class GameRepository : IGameRepository
{

    private readonly BowlingDbContext BowlingDbContext;

    public GameRepository(BowlingDbContext bowlingDbContext)
    {
        this.BowlingDbContext = bowlingDbContext;
    }

    public async Task<Game> AddAsync(Game game)
    {
        await this.BowlingDbContext.Games.AddAsync(game);
        await this.BowlingDbContext.SaveChangesAsync();
        
        return game;
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<Game>> GetAllAsync()
    {
        return await this.BowlingDbContext.Games.ToListAsync();
    }

    public async Task<Game?> GetByIdAsync(int id)
    {
        return await this.BowlingDbContext.Games.FindAsync(id);
    }

    public Task UpdateAsync(Game entity)
    {
        throw new NotImplementedException();
    }
}
