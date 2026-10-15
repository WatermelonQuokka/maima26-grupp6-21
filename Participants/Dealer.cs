namespace Tjugoett.Participants;

public class Dealer : Participant
{
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
}