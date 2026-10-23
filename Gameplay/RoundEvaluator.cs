using TwentyOne.Cards;
using TwentyOne.Participants;

namespace TwentyOne.Gameplay;

public class RoundEvaluator
{
    private RuleSet _ruleSet = new RuleSet();

    // Constructor
    public RoundEvaluator(RuleSet ruleSet)
    {
        _ruleSet = ruleSet;
    }

    public RoundResult Evaluate(Hand playerHand, Hand dealerHand)
    {
        int playerTotal = playerHand.GetTotalValue();
        int dealerTotal = dealerHand.GetTotalValue();

        // 1. If player busts, player loses
        if (playerHand.IsBust())
        {
            return RoundResult.Lose;
        }

         // 2. Check Natural 21 scenarios (Natural beats multi-card 21)
        if (playerHand.IsNatural && dealerHand.IsNatural)
        {
            return RoundResult.Push; // Both have Natural 21
        }

        if (playerHand.IsNatural)
        {
            return RoundResult.Blackjack; // Player Natural beats everything (including dealer 3-card 21)
        }

        if (dealerHand.IsNatural)
        {
            return RoundResult.Lose; // Dealer Natural beats player multi-card 21
        }

        // 3. Check if dealer busts
        if (dealerHand.IsBust())
        {
            return RoundResult.Win;
        }

        // Rule: Natural 21 beats 3-card 21
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

        // Higher points win
        if (playerTotal > dealerTotal)
        {
            return RoundResult.Win;
        }
        else if (playerTotal < dealerTotal)
        {
            return RoundResult.Lose; // Player loses if their total is lower than the dealer's
        }
        else
        {
            return RoundResult.Push; // Push only when both totalsare exactly equal
        }
    }
}
