using Tjugoett.Cards;

namespace Tjugoett.Gameplay;

public interface IMoveDecider
{
    Move DecideMove(Hand playerHand, Card dealerUpCard);
}