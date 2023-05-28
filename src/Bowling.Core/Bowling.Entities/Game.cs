namespace Bowling.Entities;

public class Game
{
    public int Id { get; set; }
    public int CurrentIndex { get; set; }
    public Frame[] Frames { get; set; }
    public string Player { get; set; }

    public Game()
    {
        CurrentIndex = 0;
        Frames = new Frame[10];
        Player = "Unknow Player";
    }
    
}
