namespace Tjugoett.Participants;

public class Dealer : Participant
{
    private const int DealerStandsAt = 17;
    
    public Dealer() : base("Givaren")
    {
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
        return Hand.GetTotalValue() < DealerStandsAt;
    }
}