using TwentyOne.Cards;
using TwentyOne.Participants;

namespace TwentyOne.Gameplay;

    public class RoundEvaluator
    {
        private static RuleSet _ruleSet = new RuleSet();

        // Constructor
        public RoundEvaluator(RuleSet ruleSet)
        {
            _ruleSet = ruleSet;
        }

        public static RoundResult Evaluate(Hand playerHand, Hand dealerHand)
        {
            int playerTotal = playerHand.GetTotalValue();
            int dealerTotal = dealerHand.GetTotalValue();

            // If player busts, player loses
            if (playerHand.IsBust())
            {
                return RoundResult.Lose;
            }

            // If dealer busts, player wins
            if (dealerHand.IsBust())
            {
                return RoundResult.Win;
            }

            // Rule: Natural 21 beats 3-card 21
            if (_ruleSet.NaturalBlackjackBeatsThreeCard21)
            {
                if (playerTotal == 21 && dealerTotal == 21)
                {
                    if (playerHand.IsNatural && !dealerHand.IsNatural)
                    {
                        return RoundResult.Win;
                    }
                    if (!playerHand.IsNatural && dealerHand.IsNatural)
                    {
                        return RoundResult.Lose;
                    }
                }
            }

            // Higher points win
            if (playerTotal > dealerTotal)
            {
                return RoundResult.Win;
            }
            else
            {
                return RoundResult.Lose;
            }
        }
    }
