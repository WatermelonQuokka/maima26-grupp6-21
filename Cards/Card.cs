namespace TwentyOne.Cards;


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

    public override string ToString()
    {
        return $"{SuitName()} {RankName()}";
    }

    private string SuitName()
    {
        return Suit switch
        {
            Suit.Hearts => "Hjärter",
            Suit.Diamonds => "Ruter",
            Suit.Clubs => "Klöver",
            Suit.Spades => "Spader",
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private string RankName()
    {
        return Rank switch
        {
            Rank.Jack => "Knekt",
            Rank.Queen => "Dam",
            Rank.King => "Kung",
            Rank.Ace => "Ess",
            _ => ((int)Rank).ToString()
        };
    }
}