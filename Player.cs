namespace Tjugoett.Models;

public class Player : Participant
{
    public int Wins { get; set; }

    public int Losses { get; set; }

    public int Pushes { get; set; }

    public Player(string name) : base(name)
    {
        Wins = 0;
        Losses = 0;
        Pushes = 0;
    }
}
