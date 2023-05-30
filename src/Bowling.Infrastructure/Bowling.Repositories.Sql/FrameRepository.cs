namespace Bowling.Repositories.Sql;

using System.Threading.Tasks;
using Bowling.Entities;
using Bowling.Interfaces;
using Microsoft.EntityFrameworkCore;

public class FrameRepository : IFrameRepository
{

    private readonly BowlingDbContext BowlingDbContext;

    public FrameRepository(BowlingDbContext bowlingDbContext)
    {
        this.BowlingDbContext = bowlingDbContext;
    }

    public async Task<Frame> AddAsync(Frame frame)
    {
        await this.BowlingDbContext.Frames.AddAsync(frame);
        await this.BowlingDbContext.SaveChangesAsync();
        
        return frame;
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<Frame>> GetAllAsync()
    {
        return await this.BowlingDbContext.Frames.ToListAsync();
    }

    public async Task<Frame?> GetByIdAsync(int id)
    {
        return await this.BowlingDbContext.Frames.FindAsync(id);
    }

    public async Task<Frame?> GetFrameByGameAndNumberAsync(int gameId, int number)
    {
        return await this.BowlingDbContext.Frames.Where(frame => 
            frame.GameId == gameId && frame.FrameNumber == number).FirstOrDefaultAsync();
    }

    public async Task<IList<Frame>> GetFramesByGameAsync(int gameId)
    {
        return await this.BowlingDbContext.Frames
            .Where(frame => frame.GameId == gameId)
            .OrderBy(frame => frame.FrameNumber)
            .ToListAsync();
    }

    public async Task UpdateAsync(Frame entity)
    {
        var frame = await this.BowlingDbContext.Frames.FindAsync(entity.Id);
        
        if (frame == null)
        {
            throw new Exception("Not found");
        }

        frame.FrameNumber = entity.FrameNumber;
        frame.IsFilled = entity.IsFilled;
        frame.Score = entity.Score;

        await this.BowlingDbContext.SaveChangesAsync();
    }
}
