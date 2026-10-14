using Tjugoett.Participants;

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
        int playerCount = ConsoleUi.ReadInt($"Välj antal spelare ({MinPlayers}-{MaxPlayers}): ", MinPlayers, MaxPlayers);
    }
}