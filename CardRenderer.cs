using System;
using System.Collections.Generic;

namespace Tjugoett.Models;

public class CardRenderer
{
    public void ShowCard(Card card)
    {
        ShowCards(new List<Card> { card });
    }

    public void ShowCards(List<Card> cards)
    {
        if (cards.Count == 0)
        {
            Console.WriteLine("Inga kort att visa.");
            return;
        }

        string[][] cardLines = new string[cards.Count][];

        for (int i = 0; i < cards.Count; i++)
        {
            cardLines[i] = CreateCardLines(cards[i]);
        }

        for (int line = 0; line < 7; line++)
        {
            for (int card = 0; card < cards.Count; card++)
            {
                ConsoleColor color = GetSuitColor(cards[card].Suit);

                Console.ForegroundColor = color;
                Console.Write(cardLines[card][line] + "  ");
                Console.ResetColor();
            }

            Console.WriteLine();
        }
    }

    private string[] CreateCardLines(Card card)
    {
        string rank = GetRankText(card.Rank);
        string symbol = GetSuitSymbol(card.Suit);

        return new string[]
        {
            "┌─────────┐",
            $"│ {rank,-2}      │",
            "│         │",
            $"│    {symbol}    │",
            "│         │",
            $"│      {rank,2} │",
            "└─────────┘"
        };
    }

    private string GetSuitSymbol(Suit suit)
    {
        return suit switch
        {
            Suit.Hearts => "♥",
            Suit.Diamonds => "♦",
            Suit.Clubs => "♣",
            Suit.Spades => "♠",
            _ => "?"
        };
    }

    private ConsoleColor GetSuitColor(Suit suit)
    {
        return suit switch
        {
            Suit.Hearts => ConsoleColor.Red,
            Suit.Diamonds => ConsoleColor.Red,
            Suit.Clubs => ConsoleColor.White,
            Suit.Spades => ConsoleColor.White,
            _ => ConsoleColor.Gray
        };
    }

    private string GetRankText(Rank rank)
    {
        return rank switch
        {
            Rank.Ace => "A",
            Rank.Jack => "J",
            Rank.Queen => "Q",
            Rank.King => "K",
            _ => ((int)rank).ToString()
        };
    }
}

