
using System;
using System.Collections.Generic;

namespace Tjugoett.Models;

public class ConsoleUi
{
    private readonly CardRenderer cardRenderer = new CardRenderer();

    public void ShowWelcome()
    {
        Console.WriteLine("==============================");
        Console.WriteLine("       VÄLKOMMEN TILL");
        Console.WriteLine("          TJUGOETT");
        Console.WriteLine("==============================");
    }

    public int AskStartOption(bool hasSavedPlayers)
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("========== HUVUDMENY ==========");

            if (hasSavedPlayers)
            {
                Console.WriteLine("1. Fortsätt med tidigare spelare");
            }
            else
            {
                Console.WriteLine("1. Fortsätt med tidigare spelare (inga sparade spelare)");
            }

            Console.WriteLine("2. Starta ett nytt spel");
            Console.WriteLine("3. Avsluta");
            Console.Write("Välj ett alternativ (1-3): ");

            string? input = Console.ReadLine();

            if (input == "1" && hasSavedPlayers)
                return 1;

            if (input == "2")
                return 2;

            if (input == "3")
                return 3;

            Console.WriteLine("Ogiltigt val. Försök igen.");
        }
    }

    public void ShowPlayerHand(Player player)
    {
        Console.WriteLine();
        Console.WriteLine($"--- {player.Name} ---");

        cardRenderer.ShowCards(
            new List<Card>(player.Hand.Cards)
        );

        Console.WriteLine($"Poäng: {player.Hand.GetValue()}");
    }

    public int AskNumberOfPlayers()
    {
        while (true)
        {
            Console.Write("Hur många spelare ska spela (1-5)? ");

            string? input = Console.ReadLine();

            if (int.TryParse(input, out int number) &&
                number >= 1 && number <= 5)
            {
                return number;
            }

            Console.WriteLine("Ogiltigt val. Ange ett tal mellan 1 och 5.");
        }
    }

    public string AskPlayerName(int playerNumber)
    {
        while (true)
        {
            Console.Write($"Ange namn för spelare {playerNumber}: ");

            string? name = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(name))
            {
                return name.Trim();
            }

            Console.WriteLine("Namnet får inte vara tomt.");
        }
    }

    public Move AskMove(string playerName)
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine($"{playerName}, vad vill du göra?");
            Console.WriteLine("1. Köpa kort");
            Console.WriteLine("2. Stanna");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    return Move.Hit;

                case "2":
                    return Move.Stand;

                default:
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                    break;
            }
        }
    }

    public void ShowHand(string name, Hand hand)
    {
        Console.WriteLine($"{name}: {hand}");
        Console.WriteLine($"Handens värde: {hand.GetValue()}");
    }

    public void ShowDealerHiddenCard(Dealer dealer)
    {
        Console.WriteLine();
        Console.WriteLine("--- Givaren ---");

        Card visibleCard = dealer.Hand.Cards[0];

        cardRenderer.ShowCard(visibleCard);

        Console.WriteLine("[Ett kort är dolt]");
    }

    public void ShowDealerHand(Dealer dealer)
    {
        Console.WriteLine();
        Console.WriteLine("--- Givaren ---");

        cardRenderer.ShowCards(
            new List<Card>(dealer.Hand.Cards)
        );

        Console.WriteLine($"Poäng: {dealer.Hand.GetValue()}");
    }

    public void ShowResult(Player player, Outcome outcome)
    {
        Console.WriteLine();

        switch (outcome)
        {
            case Outcome.Win:
                Console.WriteLine($"{player.Name} vann!");
                break;

            case Outcome.Lose:
                Console.WriteLine($"{player.Name} förlorade.");
                break;

            case Outcome.Push:
                Console.WriteLine($"{player.Name} fick lika!");
                break;

            case Outcome.Blackjack:
                Console.WriteLine($"{player.Name} fick naturlig tjugoett!");
                break;
        }

        Console.WriteLine(
            $"Vinster: {player.Wins} | " +
            $"Förluster: {player.Losses} | " +
            $"Lika: {player.Pushes}");
    }

    public bool AskPlayAgain()
    {
        while (true)
        {
            Console.WriteLine();
            Console.Write("Vill ni spela en runda till? (j/n): ");

            string? input = Console.ReadLine()?.Trim().ToLower();

            if (input == "j")
                return true;

            if (input == "n")
                return false;

            Console.WriteLine("Svara med j eller n.");
        }
    }

    public void ShowGoodbye()
    {
        Console.WriteLine();
        Console.WriteLine("Tack för att ni spelade Tjugoett!");
    }
}