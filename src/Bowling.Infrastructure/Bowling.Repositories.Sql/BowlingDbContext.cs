using System.Text.Json;
using Bowling.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
        modelBuilder.Entity<Frame>()
        .Property(e => e.Rolls)
        .HasConversion(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
            v => JsonSerializer.Deserialize<List<int>>(v, (JsonSerializerOptions)null),
            new ValueComparer<IList<int>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => (IList<int>) c.ToList()));

        base.OnModelCreating(modelBuilder);
    }
}