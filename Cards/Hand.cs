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
        foreach (var card in _cards)
        {
            total += (int)card.Rank;
        }
        return total;
    }

    // Upptäcker naturlig tjugoett (exakt två kort med värdet 21)
    public bool IsNatural => _cards.Count == 2 && GetValue() == 21;

    // Kontrollerar om handen har spruckit (värde över 21)
    public bool IsBust()
    {
        return GetValue() > 21;
    }
}
