namespace Bowling.Entities;

using System.ComponentModel.DataAnnotations;

public class Game
{
    [Key]
    public int Id { get; set; }
    public int CurrentFrame { get; set; }
    public IList<Frame> Frames { get; set; } = new List<Frame>();
    public Player Player { get; set; } = new Player();
    
}
