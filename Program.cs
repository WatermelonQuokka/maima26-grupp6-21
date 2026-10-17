using Tjugoett.Gameplay;
using Tjugoett.UI;
try
{
    Game game = new Game();
    // Huvudslinga för spelet
    bool playAgain;
    do
    {
       game.Run();
       // Frågar om spelaren vill spela igen
       playAgain = ConsoleUi.AskToPlayAgain(); 
    } while (playAgain);
    // Avslutningsmeddelande
    Console.WriteLine("\nTack för att du spelade Tjugoett! Hej då!");  
}
catch (EndOfStreamException)
{
    Console.WriteLine("Inmatningen avslutades. Hej då!");
}
