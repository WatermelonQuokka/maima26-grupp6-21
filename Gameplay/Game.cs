using Tjugoett.Cards;
using Tjugoett.Participants;
using Tjugoett.UI;

namespace Tjugoett.Gameplay;

public class Game
{
    private const int MinPlayers = 1;
    private const int MaxPlayers = 5;
    private const int OpeningCardCount = 2;
    private readonly List<Player> _players = new();
    private readonly Deck _deck = new();
    private readonly Dealer _dealer = new();

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
        DealOpeningCards();
        ShowDealerHand();
        foreach (Player player in _players)
        {
            PlayTurn(player);
        }
    }

    // Not finished only placeholder - story 13 adds here
    private void PlayTurn(Player player)
    {
        Console.WriteLine($"{player.Name}, det är din tur.");
    }
    // deals two opening cards to each player and the dealer
    private void DealOpeningCards()
    {
        foreach (Player player in _players)
        {
            player.Hand.Clear();
        }
        _dealer.Hand.Clear();
        _deck.ReshuffleIfLow();
        _dealer.HideHoleCard();
        for (int i = 0; i < OpeningCardCount; i++)
        {
            foreach (Player player in _players)
            {
                Card card = _deck.Draw();
                player.Hand.AddCard(card);
            }
            Card dealerCard = _deck.Draw();
            _dealer.Hand.AddCard(dealerCard);
        }
    }

    private void ShowDealerHand()
    {
        IReadOnlyList<Card> cards = _dealer.Hand.GetCards();
        Console.WriteLine($"{_dealer.Name}s hand:");
        if(!_dealer.IsHoleCardRevealed)
        {
            Console.WriteLine(cards[0]);
            Console.WriteLine("Dolt kort");
        }
        else
        {
            foreach(Card card in cards)
            {
                Console.WriteLine(card);
            }
        }
    }
}
