namespace Tjugoett.Models;

public class Hand
{
    private readonly List<Card> cards = new List<Card>();

    public IReadOnlyList<Card> Cards => cards.AsReadOnly();

    public void AddCard(Card card)
    {
        cards.Add(card);
    }

    public void Clear()
    {
        cards.Clear();
    }

    public int GetValue()
    {
        int total = 0;
        int aceCount = 0;

        foreach (Card card in cards)
        {
            total += card.GetValue();

            if (card.Rank == Rank.Ace)
            {
                aceCount++;
            }
        }

        while (total > 21 && aceCount > 0)
        {
            total -= 10;
            aceCount--;
        }

        return total;
    }

    public bool IsSoft()
    {
        int total = 0;
        int aceCount = 0;

        foreach (Card card in cards)
        {
            total += card.GetValue();

            if (card.Rank == Rank.Ace)
            {
                aceCount++;
            }
        }

        while (total > 21 && aceCount > 0)
        {
            total -= 10;
            aceCount--;
        }

        return aceCount > 0;
    }

    public bool IsBust()
    {
        return GetValue() > 21;
    }

    public bool IsBlackjack()
    {
        return cards.Count == 2 && GetValue() == 21;
    }

    public override string ToString()
    {
        return string.Join(" ", cards);
    }
}
