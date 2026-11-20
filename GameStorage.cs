using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Tjugoett.Models;

public class GameStorage
{
    private readonly string filePath =
        Path.Combine(Directory.GetCurrentDirectory(), "players.json");

    public void SavePlayers(List<Player> players)
    {
        List<PlayerSaveData> data = new List<PlayerSaveData>();

        foreach (Player player in players)
        {
            data.Add(new PlayerSaveData
            {
                Name = player.Name,
                Wins = player.Wins,
                Losses = player.Losses,
                Pushes = player.Pushes
            });
        }

        string json = JsonSerializer.Serialize(
            data,
            new JsonSerializerOptions { WriteIndented = true }
        );

        File.WriteAllText(filePath, json);
    }

    public List<Player> LoadPlayers()
    {
        List<Player> players = new List<Player>();

        if (!File.Exists(filePath))
        {
            return players;
        }

        try
        {
            string json = File.ReadAllText(filePath);

            List<PlayerSaveData>? data =
                JsonSerializer.Deserialize<List<PlayerSaveData>>(json);

            if (data == null)
            {
                return players;
            }

            foreach (PlayerSaveData savedPlayer in data)
            {
                if (string.IsNullOrWhiteSpace(savedPlayer.Name))
                {
                    continue;
                }

                Player player = new Player(savedPlayer.Name)
                {
                    Wins = savedPlayer.Wins,
                    Losses = savedPlayer.Losses,
                    Pushes = savedPlayer.Pushes
                };

                players.Add(player);
            }
        }
        catch (JsonException)
        {
            Console.WriteLine("Sparfilen innehåller ogiltig information.");
        }
        catch (IOException)
        {
            Console.WriteLine("Kunde inte läsa sparfilen.");
        }

        return players;
    }
}

public class PlayerSaveData
{
    public string Name { get; set; } = "";

    public int Wins { get; set; }

    public int Losses { get; set; }

    public int Pushes { get; set; }
}
