using Tjugoett.Gameplay;

try
{
    Game game = new Game();
    game.Run();
}
catch (EndOfStreamException)
{
    Console.WriteLine("Inmatningen avslutades. Hej då!");
}