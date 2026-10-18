using Tjugoett.Gameplay;
using Tjugoett.UI;
try
{
    Game game = new Game();
    game.Run();
}       
catch (EndOfStreamException)
{
    Console.WriteLine("Inmatningen avslutades. Hej då!");
}
