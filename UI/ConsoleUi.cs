namespace Tjugoett.Cards;

public class ConsoleUi
{
    public static int ReadInt(string prompt, int min, int max)
{
    while (true)
    {
        Console.Write(prompt);
        string? input = Console.ReadLine();

        if (input == null)
    {
        throw new EndOfStreamException("Inmatningen avslutades innan ett värde kunde läsas.");
    }

        if (int.TryParse(input, out int choice) && choice >= min && choice <= max)
            {
                return choice;
            }
            else
            {
                Console.WriteLine($"Ogiltig inmatning. Vänligen ange ett heltal mellan {min} och {max}.");
            }

    }
}

}