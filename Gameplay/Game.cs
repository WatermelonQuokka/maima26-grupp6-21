using TwentyOne.Cards;
using TwentyOne.Participants;
using TwentyOne.UI;
using TwentyOne.Storage;
using TwentyOne.Deciders;

namespace TwentyOne.Gameplay;

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
        // Add players only once at the begining
        AddPlayers();
        bool playAgain;
        // Main game loop for rounds
        do
        {
            Round round = new(_players, _deck, _dealer);
            round.Play();
            GameStorage.Save(_savedPlayers);
            ConsoleUi.ShowStatistics(_players);
            // Ask if players want to play another round
            playAgain = ConsoleUi.AskToPlayAgain();
        } while (playAgain);
        ConsoleUi.ShowGoodbyeMessage();
    }

    // asks user choice for amount of players
    // then asks user if human or computer player
    // then with a loop asks every player their name
    // if the name is already taken -> error message and loops again for another name
    // computers get automatic names
    private void AddPlayers()
    {
        int computerCount = 0;
        int playerCount = ConsoleUi.AskPlayerCount(MinPlayers, MaxPlayers);

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
                        ConsoleUi.ShowNameTakenError();
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
                IMoveDecider moveDecider;
                string name;
                do
                {
                    computerCount++;
                    name = $"Dator {computerCount}";
                }
                while (IsNameTaken(name));
                string strategyChoice = ConsoleUi.ReadChoice($"{name}: Vilken strategi vill du välja? Simpel eller Grund (s/g): ", new List<string> { "s", "simpel", "g", "grund" });
                if (strategyChoice == "s" || strategyChoice == "simpel")
                {
                    moveDecider = new SimpleMoveDecider();
                }
                else
                {
                    moveDecider = new BasicStrategyMoveDecider();
                }
                _players.Add(new Player(name, moveDecider));
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
