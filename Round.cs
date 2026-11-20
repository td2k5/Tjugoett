using System;
using System.Collections.Generic;
using Tjugoett.Models;

namespace Tjugoett.Models;

public class Round
{
    private readonly Deck deck;
    private readonly Dealer dealer;
    private readonly ConsoleUi ui;

    public Round(Deck deck, Dealer dealer, ConsoleUi ui)
    {
        this.deck = deck;
        this.dealer = dealer;
        this.ui = ui;
    }

    public void Play(List<Player> players)
    {
        deck.PrepareNextRound();

        dealer.ClearHand();

        foreach (Player player in players)
        {
            player.ClearHand();
        }
        foreach (Player player in players)
        {
            player.ReceiveCard(deck.DealCard());
            player.ReceiveCard(deck.DealCard());
        }
        dealer.ReceiveCard(deck.DealCard());
        dealer.ReceiveCard(deck.DealCard());

        Console.WriteLine();
        Console.WriteLine("========== NY RUNDA ==========");

        foreach (Player player in players)
        {
            ui.ShowPlayerHand(player);

            if (player.Hand.IsBlackjack())
            {
                Console.WriteLine("Naturlig tjugoett!");
            }
        }
        ui.ShowDealerHiddenCard(dealer);

        foreach (Player player in players)
        {
            if (player.Hand.IsBlackjack())
            {
                continue;
            }

            bool finished = false;

            while (!finished && !player.Hand.IsBust())
            {
                Move move = ui.AskMove(player.Name);

                if (move == Move.Hit)
                {
                    player.ReceiveCard(deck.DealCard());

                    ui.ShowPlayerHand(player);

                    if (player.Hand.IsBust())
                    {
                        Console.WriteLine("Du har spruckit!");
                        finished = true;
                    }
                }
                else
                {
                    Console.WriteLine($"{player.Name} stannar.");
                    finished = true;
                }
            }
        }

        ui.ShowDealerHand(dealer);

        Console.WriteLine();

        Console.WriteLine("Givaren vänder upp sitt dolda kort!");

        ui.ShowDealerHand(dealer);
       
        while (dealer.ShouldHit())
        {
            Console.WriteLine("Givaren köper ett kort.");

            dealer.ReceiveCard(deck.DealCard());

           ui.ShowDealerHand(dealer);
        }

        if (dealer.Hand.IsBust())
        {
            Console.WriteLine("Givaren har spruckit!");
        }
        else
        {
            Console.WriteLine("Givaren stannar.");
        }

        foreach (Player player in players)
        {
            Outcome outcome = DetermineOutcome(player, dealer);

            switch (outcome)
            {
                case Outcome.Win:
                case Outcome.Blackjack:
                    player.Wins++;
                    break;

                case Outcome.Lose:
                    player.Losses++;
                    break;

                case Outcome.Push:
                    player.Pushes++;
                    break;
            }

            ui.ShowResult(player, outcome);
        }
    }

    private Outcome DetermineOutcome(Player player, Dealer dealer)
    {
        Hand playerHand = player.Hand;
        Hand dealerHand = dealer.Hand;

        int playerValue = playerHand.GetValue();
        int dealerValue = dealerHand.GetValue();

        bool playerBlackjack = playerHand.IsBlackjack();
        bool dealerBlackjack = dealerHand.IsBlackjack();

        if (playerHand.IsBust())
        {
            return Outcome.Lose;
        }

        if (playerBlackjack && dealerBlackjack)
        {
            return Outcome.Push;
        }

        if (playerBlackjack)
        {
            return Outcome.Blackjack;
        }

        if (dealerBlackjack)
        {
            return Outcome.Lose;
        }

        if (dealerHand.IsBust())
        {
            return Outcome.Win;
        }

        if (playerValue > dealerValue)
        {
            return Outcome.Win;
        }

        if (playerValue < dealerValue)
        {
            return Outcome.Lose;
        }

        return Outcome.Push;
    }
}
