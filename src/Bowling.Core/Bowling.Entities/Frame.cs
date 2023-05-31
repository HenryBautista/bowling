namespace Bowling.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Frame
{
    [Key]
    public int Id { get; set; }
    public IList<int> Rolls { get; set;} = new List<int>();
    public int Score { get; set; }
    public int GameId { get; set; }
    public bool IsFilled { get; set; } = false;
    public int FrameNumber { get; set; }

}