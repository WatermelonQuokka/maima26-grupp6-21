using TwentyOne.Deciders;

namespace TwentyOne.Participants;

public class Player : Participant
{
    public int Wins { get; private set; }
    public int Losses { get; private set; }
    public int Pushes { get; private set; }
    public IMoveDecider MoveDecider { get; }
    
    public Player(string name, IMoveDecider moveDecider) : base(name)
    {
        MoveDecider = moveDecider;
    }
    public void AddWin()
    {
        Wins++;
    }
    public void AddLoss()
    {
        Losses++;
    }
    public void AddPush()
    {
        Pushes++;
    }

    internal void RestoreStats(int wins, int losses, int pushes)
    {
        Wins = wins;
        Losses = losses;
        Pushes = pushes;
    }
}