namespace Tjugoett.Models;

public class Card
{
    public Suit Suit { get; }
    public Rank Rank { get; }

    public Card(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
    }

    public int GetValue()
    {
        if (Rank == Rank.Ace)
            return 11;

        if (Rank >= Rank.Jack)
            return 10;

        return (int)Rank;
    }

    public override string ToString()
    {
        string suit = Suit switch
        {
            Suit.Hearts => "♥",
            Suit.Diamonds => "♦",
            Suit.Clubs => "♣",
            Suit.Spades => "♠",
            _ => "?"
        };

        string rank = Rank switch
        {
            Rank.Ace => "A",
            Rank.Jack => "J",
            Rank.Queen => "Q",
            Rank.King => "K",
            _ => ((int)Rank).ToString()
        };

        return $"{suit}{rank}";
    }
}