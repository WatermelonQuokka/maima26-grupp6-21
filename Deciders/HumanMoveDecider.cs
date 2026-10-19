using Tjugoett.Cards;
using Tjugoett.UI;
using Tjugoett.Gameplay;

namespace Tjugoett.Deciders;

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