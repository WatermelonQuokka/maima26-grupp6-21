using System.Collections.Generic;

namespace Tjugoett.Cards;

public class Hand
{
    private readonly List<Card> _cards = new List<Card>();

    public void AddCard(Card card)
    {
        if (card != null)
        {
            _cards.Add(card);
        }
    }

    public void Clear()
    {
        _cards.Clear();
    }

    public IReadOnlyList<Card> GetCards()
    {
        return _cards.AsReadOnly();
    }
    // Beräknar det totala värdet för handen
    public int GetValue()
{
    int total = 0;
    int aceCount = 0;

    foreach (var card in _cards)
    {
        if (card.Rank == Rank.Ace)
        {
            aceCount++;
            total += 11; // Startvärde för ess i Tjugoett
        }
        else if (card.Rank == Rank.Jack || card.Rank == Rank.Queen || card.Rank == Rank.King)
        {
            total += 10; // Klädda kort är värda 10
        }
        else
        {
            total += (int)card.Rank; // Värdet för övriga nummerkort
        }
    }

    // Om totalen överstiger 21 ändras essets värde till 1 (minskar med 10)
    while (total > 21 && aceCount > 0)
    {
        total -= 10;
        aceCount--;
    }

    return total;
}

    // Upptäcker naturlig tjugoett (exakt två kort med värdet 21)
    public bool IsNatural => _cards.Count == 2 && GetTotalValue() == 21;

    // Kontrollerar om handen har spruckit (värde över 21)
    public bool IsBust()
    {
        return GetValue() > 21;
    }
    public int GetTotalValue()
    {
        return GetValue();
    }
}
