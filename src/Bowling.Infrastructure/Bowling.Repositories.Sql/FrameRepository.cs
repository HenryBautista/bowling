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

    public async Task UpdateAsync(Frame entity)
    {
        var frame = await this.BowlingDbContext.Frames.FindAsync(entity.Id);
        
        if (frame == null)
        {
            throw new Exception("Not found");
        }

        frame.FirstRoll = entity.FirstRoll;
        frame.SecondRoll = entity.SecondRoll;
        frame.Score = entity.Score;

        await this.BowlingDbContext.SaveChangesAsync();
    }
}
