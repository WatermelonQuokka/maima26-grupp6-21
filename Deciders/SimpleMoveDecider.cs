using TwentyOne.Cards;
using TwentyOne.Gameplay;

namespace TwentyOne.Deciders;

public class SimpleMoveDecider : IMoveDecider
{
    private const int ComputerStandsAt = 17;

    public Move DecideMove(Hand playerHand, Card dealerUpCard)
    {
        if (playerHand.GetTotalValue() < ComputerStandsAt)
        {
            return Move.Hit;
        }
        else
        {
            return Move.Stand;
        }
    }
}