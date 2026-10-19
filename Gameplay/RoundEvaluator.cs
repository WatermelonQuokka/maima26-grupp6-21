using System.Runtime.CompilerServices;
using Tjugoett.Gameplay;

namespace Tjugoett.Gameplay
{
    public class RoundEvaluator
    {
        private RuleSet _ruleSet;

        // Constructor
        public RoundEvaluator(RuleSet ruleSet)
        {
            _ruleSet = ruleSet;
        }

        // Check if player wins
        public bool PlayerWins(Cards.Hand playerHand, Cards.Hand dealerHand)
        {
            int playerTotal = playerHand.GetTotalValue();
            int dealerTotal = dealerHand.GetTotalValue();

            // If player busts, player loses
            if (playerHand.IsBust())
            {
                return false;
            }

            // If dealer busts, player wins
            if (dealerHand.IsBust())
            {
                return true;
            }

            // Rule: Natural 21 beats 3-card 21
            if (_ruleSet.NaturalBlackjackBeatsThreeCard21)
            {
                if (playerTotal == 21 && dealerTotal == 21)
                {
                    if (playerHand.IsNatural && !dealerHand.IsNatural)
                    {
                        return true;
                    }
                    if (!playerHand.IsNatural && dealerHand.IsNatural)
                    {
                        return false;
                    }
                }
            }

            // Higher points win
            if (playerTotal > dealerTotal)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
