using Tjugoett.Gameplay;

namespace Tjugoett.Participants;

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
}