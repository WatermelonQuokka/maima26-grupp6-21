namespace Tjugoett.Cards;


// single play card with no value

public class Card
{
    public Card(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
    }

    public Suit Suit { get; }

    public Rank Rank { get; }
}