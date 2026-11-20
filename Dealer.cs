namespace Tjugoett.Models;

public class Dealer : Participant
{
    public Dealer() : base("Givaren")
    {
    }

    public bool ShouldHit()
    {
        return Hand.GetValue() <= 16;
    }
}
