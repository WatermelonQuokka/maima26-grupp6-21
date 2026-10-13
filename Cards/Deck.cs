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
        Shuffle();
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

    // Fisher-Yates shuffle: start backwards and swap each card with a random card at or before it
    private void Shuffle()
    {
        for (int i = Count - 1; i > 0; i--)
        {
            int j = Random.Shared.Next(0, i + 1);
            Card temp = _cards[i];
            _cards[i] = _cards[j];
            _cards[j] = temp;
        }
    }
}