namespace Bowling.Entities;

using System.ComponentModel.DataAnnotations;
public class Frame
{
    [Key]
    public int Id { get; set; }
    public int FirstRoll { get; set; }
    public int SecondRoll { get; set; }
    public int Score { get; set; }
    public int GameId { get; set; }
}