using System.Collections.Generic;
namespace Blackjack;
public class Hand
{
    private readonly List<Card> _cards = new List<Card>();
    public void AddCard(Card card)
    {
        _cards.Add(card);
    }
}