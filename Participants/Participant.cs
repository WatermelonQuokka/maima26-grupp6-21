using Twentyone.Cards;

namespace Twentyone.Participants;

public abstract class Participant
{
    protected Participant(string name)
    {
        Name = name;
    }
    public string Name { get; }
    public Hand Hand { get; } = new();
}