using TwentyOne.Cards;
using TwentyOne.Gameplay;

namespace TwentyOne.Deciders;

public interface IMoveDecider
{
    Move DecideMove(Hand playerHand, Card dealerUpCard);
}