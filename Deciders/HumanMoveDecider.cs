using Twentyone.Cards;
using Twentyone.UI;
using Twentyone.Gameplay;

namespace Twentyone.Deciders;

public class HumanMoveDecider : IMoveDecider
{
    public Move DecideMove(Hand playerHand, Card dealerUpCard)
    {
        string answer = ConsoleUi.ReadChoice("Köpa eller stanna? (k/s): ", new List<string> { "k", "köpa", "s", "stanna"});

        return answer switch
        {
            "k" or "köpa" => Move.Hit,
            "s" or "stanna" => Move.Stand,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}