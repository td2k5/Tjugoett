using Tjugoett.Models;

namespace Tjugoett.Models;

public class Deck
{
    private readonly List<Card> cards;
    private readonly Random random;

    public int CardsRemaining => cards.Count;

    public Deck(Random random)
    {
        this.random = random;
        cards = new List<Card>();

        CreateDeck();
        Shuffle();
    }

    private void CreateDeck()
    {
        cards.Clear();

        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                cards.Add(new Card(suit, rank));
            }
        }
    }

    public void Shuffle()
    {
        for (int i = cards.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);

            (cards[i], cards[j]) = (cards[j], cards[i]);
        }
    }

    public Card DealCard()
    {
        if (cards.Count == 0)
        {
            CreateDeck();
            Shuffle();
        }

        Card card = cards[0];
        cards.RemoveAt(0);

        return card;
    }

    public void PrepareNextRound()
    {
        if (cards.Count < 15)
        {
            CreateDeck();
            Shuffle();
        }
    }
}
