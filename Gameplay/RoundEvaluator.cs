using Tjugoett.Cards;
using Tjugoett.Participants;

namespace Tjugoett.Gameplay;

public static class RoundEvaluator
{
    public static RoundResult Evaluate(Hand playerHand, Hand dealerHand)
    {
        if (playerHand.IsNatural && dealerHand.IsNatural)
        {
            return RoundResult.Push;
        }
        if (playerHand.IsNatural)
        {
            return RoundResult.Blackjack;
        }
        if (dealerHand.IsNatural)
        {
            return RoundResult.Lose;
        }

        if (playerHand.IsBust())
        {
            return RoundResult.Lose;
        }

        if (dealerHand.IsBust())
        {
            return RoundResult.Win;
        }

        int playerTotal = playerHand.GetTotalValue();
        int dealerTotal = dealerHand.GetTotalValue();

        if (playerTotal > dealerTotal)
        {
            return RoundResult.Win;
        }
        if (playerTotal < dealerTotal)
        {
            return RoundResult.Lose;
        }

        return RoundResult.Push;
    }
}