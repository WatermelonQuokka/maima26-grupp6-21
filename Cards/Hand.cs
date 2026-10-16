
using System.Security.Cryptography.X509Certificates;

namespace Tjugoett.Cards;
public class Hand
{
    private readonly List<Card> _cards = new List<Card>();
    public void AddCard(Card card)
    {
        _cards.Add(card);
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
}