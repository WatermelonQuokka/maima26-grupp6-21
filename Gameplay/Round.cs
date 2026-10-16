using Tjugoett.Cards;
using Tjugoett.Participants;
using Tjugoett.UI;

namespace Tjugoett.Gameplay;

public class Round
{
    private readonly List<Player> _players;
    private readonly Deck _deck;
    private readonly Dealer _dealer;
    private const int OpeningCardCount = 2;

    public Round(List<Player> players, Deck deck, Dealer dealer)
    {
        _players = players;
        _deck = deck;
        _dealer = dealer;
    }

    public void Play()
    {
        DealOpeningCards();
        ShowDealerHand();
        foreach (Player player in _players)
        {
            PlayTurn(player);
        }
        PlayDealerTurn();
    }

    // ask the players movedecider for hit or stand
    // deals a card on hit, repeats until player stand or bust
    private void PlayTurn(Player player)
    {
        Console.WriteLine($"{player.Name}, det är din tur.");
        if (player.Hand.IsNatural)
        {
            Console.WriteLine($"{player.Name} har Naturlig tjugoett! och hoppar över sin tur.");
        }
        Card dealerUpCard = _dealer.Hand.GetCards()[0];
        Console.WriteLine($"{_dealer.Name} visar: {dealerUpCard}");
        Move move;
        ConsoleUi.ShowHand(player);
        do
        {
            move = player.MoveDecider.DecideMove(player.Hand, dealerUpCard);
            if (move == Move.Hit)
            {
                Card card = _deck.Draw();
                player.Hand.AddCard(card);
                ConsoleUi.ShowHand(player);
            }
        }
        while (move == Move.Hit && !player.Hand.IsBust());
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
        if (!_dealer.IsHoleCardRevealed)
        {
            Console.WriteLine(cards[0]);
            Console.WriteLine("Dolt kort");
        }
        else
        {
            foreach (Card card in cards)
            {
                Console.WriteLine(card);
            }
        }
    }

    // reveals the hole card, then draws until
    // the dealer stands on 17 or higher.
    private void PlayDealerTurn()
    {
        _dealer.RevealHoleCard();
        ShowDealerHand();
        while (_dealer.ShouldHit())
        {
            Card dealerCard = _deck.Draw();
            _dealer.Hand.AddCard(dealerCard);
            ShowDealerHand();
        }
    }    
}