namespace Tjugoett.Participants;

public class Player : Participant
{
    public int Wins { get; private set; }
    public int Losses { get; private set; }
    public int Pushes { get; private set; }
    public Player(string name) : base(name)
    {
    }
}