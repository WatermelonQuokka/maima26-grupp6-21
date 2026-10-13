namespace Tjugoett.Cards;

public class Deck
{
    private List<Card> _cards = new();

    public int Count => _cards.Count;


    public Deck()
    {
        // looping through enums suits * ranks
        // = total deck (4 * 13) = 52
        foreach(Suit suit in Enum.GetValues<Suit>())
        {
            foreach(Rank rank in Enum.GetValues<Rank>())
            {
                _cards.Add(new Card(suit, rank));
            }
        }
    }

    public Card Draw()
    {
        if (Count == 0)
        {
            throw new InvalidOperationException("Kan inte dra ett kort från en tom kortlek.");
        }
        Card card = _cards[Count - 1];
        _cards.RemoveAt(Count - 1);
        return card;
    }
}