using Twentyone.Cards;
using Twentyone.Gameplay;

namespace Twentyone.Deciders;

public interface IMoveDecider
{
    Move DecideMove(Hand playerHand, Card dealerUpCard);
}