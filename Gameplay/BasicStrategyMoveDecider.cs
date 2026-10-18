using Tjugoett.Cards;

namespace Tjugoett.Gameplay;

public class BasicStrategyMoveDecider : IMoveDecider
{
    private const Move H = Move.Hit;
    private const Move S = Move.Stand;
    private static readonly Move[][] HardTable =
    [// dealer shows 2  3  4  5  6  7  8  9  10 A
                    [H, H, H, H, H, H, H, H, H, H], // player hand 4-11
                    [H, H, S, S, S, H, H, H, H, H], // player hand 12
                    [S, S, S, S, S, H, H, H, H, H], // player hand 13-16
                    [S, S, S, S, S, S, S, S, S, S]  // player hand 17-21
    ];
    private static readonly Move[][] SoftTable =
    [// dealer shows 2  3  4  5  6  7  8  9  10 A
                    [H, H, H, H, H, H, H, H, H, H], // player hand soft 12-17
                    [S, S, S, S, S, S, S, H, H, H], // player hand soft 18
                    [S, S, S, S, S, S, S, S, S, S]  // player hand soft 19-21
    ];
    public Move DecideMove(Hand playerHand, Card dealerUpCard)
    {
        int total = playerHand.GetTotalValue();
        int column = DealerColumn(dealerUpCard);

        if (playerHand.IsSoft())
        {
            int row = SoftRow(total);
            return SoftTable[row][column];
        }
        else
        {
            int row = HardRow(total);
            return HardTable[row][column];
        }
    }

    private static int DealerColumn(Card dealerUpCard)
    {
        return dealerUpCard.Rank switch
        {
            Rank.Jack or Rank.Queen or Rank.King => 8,
            Rank.Ace => 9,
            _ => (int)dealerUpCard.Rank - 2
        };
    }

    private static int SoftRow(int total)
    {
        return total switch
        {
            >= 12 and <= 17 => 0,
                         18 => 1,
            >= 19 and <= 21 => 2,
            _ => throw new ArgumentOutOfRangeException(nameof(total), total, "Finns inte med i soft-tabellen.")
        };
    }

    private static int HardRow(int total)
    {
        return total switch
        {
            >= 4 and <= 11 => 0,
                         12 => 1,
            >= 13 and <= 16 => 2,
            >= 17 and <= 21 => 3,
            _ => throw new ArgumentOutOfRangeException(nameof(total), total, "Finns inte med i hard-tabellen.")
        };
    }
}