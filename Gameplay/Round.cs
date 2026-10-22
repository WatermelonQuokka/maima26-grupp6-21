using TwentyOne.Cards;
using TwentyOne.Participants;
using TwentyOne.UI;

namespace TwentyOne.Gameplay;

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

        Console.WriteLine();

        RoundEvaluator evaluator = new RoundEvaluator(RuleSet.LoadFromFile("rules.txt"));

        foreach (Player player in _players)
    {
        RoundResult result = evaluator.Evaluate(player.Hand, _dealer.Hand);
        RecordResult(player, result);
        ConsoleUi.ShowResult(player, result);
    }
    }

    private static void RecordResult(Player player, RoundResult result)
    {
        switch (result)
        {
            case RoundResult.Win:
            case RoundResult.Blackjack:
                player.AddWin();
                break;
            case RoundResult.Lose:
                player.AddLoss();
                break;
            case RoundResult.Push:
                player.AddPush();
                break;
        }
    }

    // ask the players movedecider for hit or stand
    // deals a card on hit, repeats until player stand or bust
    private void PlayTurn(Player player)
    {
        Console.WriteLine($"{player.Name}, det är din tur.");
        Card dealerUpCard = _dealer.Hand.GetCards()[0];
        Console.WriteLine($"{_dealer.Name} visar: {dealerUpCard}");
        ConsoleUi.ShowHand(player);
        // Hoppa över turen om spelaren har en naturlig tjugoett 
        if (player.Hand.IsNatural)
        {
            Console.WriteLine($"{player.Name} har naturlig tjugoett och hoppar över sin tur.");
            return; //Avsluta metoden direkt
        }
        Move move;
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
    
        // Skriv ut "Spräckt!" om spelaren har spräckt
        if (player.Hand.IsBust())
        {
            Console.WriteLine("Spräckt!");
        }
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
        bool hideSecondCard = !_dealer.IsHoleCardRevealed;
        ConsoleUi.ShowHand(_dealer, hideSecondCard);
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