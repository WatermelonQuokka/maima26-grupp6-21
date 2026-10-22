using TwentyOne.Gameplay;
namespace TwentyOne.Participants;

public class Dealer : Participant
{
    private readonly RuleSet _ruleSet;
    
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
        if (score < 17)
        return true;
        if (score == 17 && Hand.IsSoft() && _ruleSet.DealerHitsOnSoft17)
        return true;
        return false;
    }
}