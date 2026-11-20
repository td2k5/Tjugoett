using System;
using System.Collections.Generic;

namespace Tjugoett.Models;

public class Game
{
    private readonly ConsoleUi ui;
    private readonly List<Player> players;
    private readonly Deck deck;
    private readonly Dealer dealer;
    private readonly GameStorage storage;

    public Game()
    {
        Random random = new Random();

        ui = new ConsoleUi();
        players = new List<Player>();
        deck = new Deck(random);
        dealer = new Dealer();
        storage = new GameStorage();
    }

    public void Start()
    {
        ui.ShowWelcome();

        // Läs in tidigare sparade spelare
        List<Player> savedPlayers = storage.LoadPlayers();

        // Visa huvudmenyn
        int choice = ui.AskStartOption(savedPlayers.Count > 0);

        // Avsluta spelet
        if (choice == 3)
        {
            ui.ShowGoodbye();
            return;
        }

        // Fortsätt med tidigare spelare
        if (choice == 1)
        {
            players.AddRange(savedPlayers);

            Console.WriteLine();
            Console.WriteLine("Tidigare spelare har lästs in.");

            foreach (Player player in players)
            {
                Console.WriteLine(
                    $"{player.Name}: {player.Wins} vinster, " +
                    $"{player.Losses} förluster, " +
                    $"{player.Pushes} lika."
                );
            }
        }
        // Starta ett nytt spel
        else if (choice == 2)
        {
            Console.WriteLine();
            Console.WriteLine("Ett nytt spel startas!");

            SetupPlayers();
        }

        // Skapa en ny spelrunda
        Round round = new Round(deck, dealer, ui);

        bool playing = true;

        while (playing)
        {
            round.Play(players);

            // Spara resultaten efter varje runda
            storage.SavePlayers(players);

            // Fråga om spelarna vill fortsätta
            playing = ui.AskPlayAgain();
        }

        ui.ShowGoodbye();
    }

    // Skapa nya spelare
    private void SetupPlayers()
    {
        int numberOfPlayers = ui.AskNumberOfPlayers();

        for (int i = 1; i <= numberOfPlayers; i++)
        {
            string name = ui.AskPlayerName(i);

            players.Add(new Player(name));
        }
    }
}
