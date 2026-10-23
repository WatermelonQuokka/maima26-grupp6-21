using TwentyOne.Cards;
using TwentyOne.UI;
using TwentyOne.Gameplay;

namespace TwentyOne.Deciders;

public class HumanMoveDecider : IMoveDecider
{
    public Move DecideMove(Hand playerHand, Card dealerUpCard)
    {
        return ConsoleUi.AskPlayerMove();
    }
}