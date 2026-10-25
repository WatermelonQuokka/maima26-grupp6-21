using TwentyOne.Cards;

namespace TwentyOne.Participants;

public abstract class Participant
{
    protected Participant(string name)
    {
        Name = name;
    }
    public string Name { get; }
    public Hand Hand { get; } = new();
}