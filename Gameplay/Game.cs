using Tjugoett.Cards;
using Tjugoett.Participants;
using Tjugoett.UI;
using Tjugoett.Storage;

namespace Tjugoett.Gameplay;

public class Game
{
    private const int MinPlayers = 1;
    private const int MaxPlayers = 5;
    private readonly List<Player> _players = new();
    private readonly List<Player> _savedPlayers;
    private readonly Deck _deck = new();
    private readonly Dealer _dealer = new();
    public Game()
    {
        _savedPlayers = GameStorage.Load();
    }

    public void Run()
    {
        AddPlayers();
        Round round = new(_players, _deck, _dealer);
        round.Play();
        GameStorage.Save(_savedPlayers);
    }

    // asks user choice for amount of players
    // then asks user if human or computer player
    // then with a loop asks every player their name
    // if the name is already taken -> error message and loops again for another name
    // computers get automatic names
    private void AddPlayers()
    {
        int computerCount = 0;
        int playerCount = ConsoleUi.ReadInt($"Välj antal spelare ({MinPlayers}-{MaxPlayers}): ", MinPlayers, MaxPlayers);

        for (int i = 0; i < playerCount; i++)
        {
            string choice = ConsoleUi.ReadChoice($"Spelare {i + 1}: människa eller dator? (m/d) ", new List<string> { "m", "människa", "d", "dator" });

            if (choice == "m" || choice == "människa")
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
                
                Player? existingPlayer = FindSavedPlayer(name);
                if (existingPlayer != null)
                {
                    _players.Add(existingPlayer);
                }
                else
                {
                    Player newPlayer = new Player(name, new HumanMoveDecider());
                    _savedPlayers.Add(newPlayer);
                    _players.Add(newPlayer);
                }
            }
            else
            {
                string name;
                do
                {
                    computerCount++;
                    name = $"Dator {computerCount}";
                }
                while (IsNameTaken(name));
                _players.Add(new Player(name, new SimpleMoveDecider()));
            }
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
    private Player? FindSavedPlayer(string name)
    {
        foreach (Player player in _savedPlayers)
        {
            if (string.Equals(player.Name, name, StringComparison.OrdinalIgnoreCase))
            {
                return player;
            }
        }
        return null;
    }
}
