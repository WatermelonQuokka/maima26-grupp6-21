using Tjugoett.Participants;
using Tjugoett.UI;

namespace Tjugoett.Gameplay;

public class Game
{
    private const int MinPlayers = 1;
    private const int MaxPlayers = 5;
    private readonly List<Player> _players = new();

    public Game()
    {
    }

    public void Run()
    {
        AddPlayers();
        PlayRound();
    }

    // asks user choice for amount of players
    // then with a loop asks every player their name
    // if the name is already taken -> error message and loops again for another name
    private void AddPlayers()
    {
        int playerCount = ConsoleUi.ReadInt($"Välj antal spelare ({MinPlayers}-{MaxPlayers}): ", MinPlayers, MaxPlayers);

        for (int i = 0; i < playerCount; i++)
        {
            string name;
            do
            {
                name = ConsoleUi.ReadNonEmptyText($"Spelare {i + 1}, ange ditt namn: ");
                if (IsNameTaken(name))
                {
                    Console.WriteLine("Det finns redan en spelare med det namnet.");
                }
            }
            while (IsNameTaken(name));
            _players.Add(new Player(name));
        }
    }

    // checks each player in the player list
    // if it contains the name, also ignores lower and uppercase
    // Example: Anna == anna
    private bool IsNameTaken(string name)
    {
        foreach (Player player in _players)
        {
            if (string.Equals(player.Name, name, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }

    private void PlayRound()
    {
        foreach(Player player in _players)
        {
            PlayTurn(player);
        }
    }

    // Not finished only placeholder - story 13 adds here
    private void PlayTurn(Player player)
    {
        Console.WriteLine($"{player.Name}, det är din tur.");
    }
}
