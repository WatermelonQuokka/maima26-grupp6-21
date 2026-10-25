using TwentyOne.Gameplay;
namespace TwentyOne.Participants;

public class Dealer : Participant
{
    private readonly RuleSet _ruleSet;
    private const int DealerStandsAt = 17 ;
    public Dealer(RuleSet? ruleSet = null) : base("Givaren")
    {
        _ruleSet = ruleSet ?? new RuleSet();
    }

    public bool IsHoleCardRevealed { get; private set; }
    
    public void RevealHoleCard()
    {
        IsHoleCardRevealed = true;
    }

    public void HideHoleCard()
    {
        IsHoleCardRevealed = false;
    }

    public bool ShouldHit()
    {
        int score = Hand.GetTotalValue();
        if (score < DealerStandsAt)
        {
            return true;
        }
        if (score == DealerStandsAt && Hand.IsSoft() && _ruleSet.DealerHitsOnSoft17)
        {
            return true;
        }
        return false;
    }
}