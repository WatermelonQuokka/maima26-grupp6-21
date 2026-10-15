using Tjugoett.Cards;

namespace Tjugoett.Participants;

public abstract class Participant
{
    protected Participant(string name)
    {
        Name = name;
    }
    public string Name { get; }
    public Hand Hand { get; } = new();
}