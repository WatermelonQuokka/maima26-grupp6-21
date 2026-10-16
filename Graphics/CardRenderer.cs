using Tjugoett.Cards;

namespace Tjugoett.Graphics;

public static class CardRenderer
{
    private const int InnerWidth = 7;
    public static string[] RenderCard(Card card, bool faceUp)
    {
        string horizontalLine = HorizontalBorder();
        if (!faceUp)
        {
            return RenderBack(horizontalLine);
        }

        string emptyLine = new string(' ', InnerWidth);

        string rank = RankText(card.Rank);
        string suit = SuitSymbol(card.Suit);
        string centeredSuit = suit.PadLeft(4).PadRight(InnerWidth);
        return [
            $"┌{horizontalLine}┐",
            $"│{rank.PadRight(InnerWidth)}│",
            $"│{emptyLine}│",
            $"│{centeredSuit}│",
            $"│{emptyLine}│",
            $"│{rank.PadLeft(InnerWidth)}│",
            $"└{horizontalLine}┘"
        ];
    }

    private static string HorizontalBorder() => new string('─', InnerWidth);

    private static string[] RenderBack(string horizontalLine)
    {
        string patternLine = new string('▒', InnerWidth);

        return [
            $"┌{horizontalLine}┐",
            $"│{patternLine}│",
            $"│{patternLine}│",
            $"│{patternLine}│",
            $"│{patternLine}│",
            $"│{patternLine}│",
            $"└{horizontalLine}┘"
        ];
    }



    private static string RankText(Rank rank) => rank switch
    {
        Rank.Ace => "A",
        Rank.Jack => "J",
        Rank.Queen => "Q",
        Rank.King => "K",
        _ => ((int)rank).ToString()
    };

    private static string SuitSymbol(Suit suit) => suit switch
    {
        Suit.Hearts => "♥",
        Suit.Diamonds => "♦",
        Suit.Clubs => "♣",
        Suit.Spades => "♠",
        _ => throw new ArgumentOutOfRangeException(nameof(suit), suit, null)
    };


}