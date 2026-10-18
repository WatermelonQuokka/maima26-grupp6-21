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
    }
}