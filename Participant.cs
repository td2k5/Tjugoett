namespace Tjugoett.Models;

public abstract class Participant
{
    public string Name { get; set; }

    public Hand Hand { get; private set; }

    protected Participant(string name)
    {
        Name = name;
        Hand = new Hand();
    }

    public void ReceiveCard(Card card)
    {
        Hand.AddCard(card);
    }

    public void ClearHand()
    {
        Hand.Clear();
    }
}
