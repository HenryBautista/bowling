using System.Text.Json;
using Bowling.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bowling.Repositories.Sql;

public class BowlingDbContext : DbContext
{
    public DbSet<Game> Games { get; set; }
    public DbSet<Frame> Frames { get; set; }
    public DbSet<Player> Players { get; set; }

    public BowlingDbContext(
        DbContextOptions<BowlingDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(
        DbContextOptionsBuilder options)
    {
        options.UseInMemoryDatabase("bowling");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var jsonSerializerOptions = new JsonSerializerOptions {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
        };

        modelBuilder.Entity<Frame>()
            .Property(frame => frame.Rolls)
            .HasConversion(
                list => JsonSerializer
                    .Serialize(
                        list,
                        jsonSerializerOptions),
                list => JsonSerializer
                    .Deserialize<List<int>>(
                        list,
                        jsonSerializerOptions));
        
        base.OnModelCreating(modelBuilder);
    }
}