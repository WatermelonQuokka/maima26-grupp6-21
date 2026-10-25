using TwentyOne.Gameplay;
using TwentyOne.UI;
try
{
    Game game = new Game();
    game.Run(); 
}
catch (EndOfStreamException)
{
    ConsoleUi.ShowInputEndedMessage();
}
