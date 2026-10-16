using System.Text.Json;
using Tjugoett.Participants;
using Tjugoett.Gameplay;

namespace Tjugoett.Storage;

public static class GameStorage
{
    private const string FolderPath = "Data";
    private const string FileName = "PlayersData.json";
    private static readonly string FilePath = Path.Combine(FolderPath, FileName);

    public static void Save(List<Player> players)
    {
        var records = new List<PlayerRecord>();
        foreach (var player in players)
        {
            records.Add(new PlayerRecord
            {
                Name = player.Name,
                Wins = player.Wins,
                Losses = player.Losses,
                Pushes = player.Pushes
            });
        }

        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(records, options);

        Directory.CreateDirectory(FolderPath);
        File.WriteAllText(FilePath, json);
    }

    public static List<Player> Load()
    {
        if (!File.Exists(FilePath))
        {
            return new List<Player>();
        }

        try
        {
            string json = File.ReadAllText(FilePath);
            var records = JsonSerializer.Deserialize<List<PlayerRecord>>(json) ?? new List<PlayerRecord>();

            List<Player> players = new List<Player>();
            foreach (var record in records)
            {
                Player player = new Player(record.Name, new HumanMoveDecider());
                player.RestoreStats(record.Wins, record.Losses, record.Pushes);
                players.Add(player);
            }

            return players;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Varning: Kunde inte tolka sparfilen ({ex.Message}). Startar med tom spelarlista.");
            return new List<Player>();
        }
    }
}