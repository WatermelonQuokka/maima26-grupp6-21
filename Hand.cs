
namespace Tjugoett.Cards;
public class Hand
{
    private readonly List<Card> _cards = new List<Card>();
    public void AddCard(Card card)
    {
        _cards.Add(card);
    }
}
public IReadOnlyList<Card> GetCards()
    {
        return _cards.AsReadOnly();
    }
    public void Clear()
    {
        _cards.Clear();
    }
    