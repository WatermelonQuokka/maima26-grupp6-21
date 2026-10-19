using Tjugoett.Cards;
using Tjugoett.Gameplay;

namespace Tjugoett.Deciders;

public interface IMoveDecider
{
    Move DecideMove(Hand playerHand, Card dealerUpCard);
}