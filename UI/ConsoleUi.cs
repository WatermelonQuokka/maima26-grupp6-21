namespace Tjugoett.UI;

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
            Console.WriteLine($"Ogiltig inmatning. Vänligen ange ett heltal mellan {min} och {max}.");

    }
}
    public static string ReadNonEmptyText(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

        if (input == null)
        {
            throw new EndOfStreamException("Inmatningen avslutades innan ett värde kunde läsas.");
        }
            if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();
                }
                Console.WriteLine($"Ogiltig inmatning. Texten får inte vara tom.");
        }
    }


public static string ReadChoice(string prompt, List<string> validOptions)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            if (input == null)
            {
                throw new EndOfStreamException("Inmatningen avslutades innan ett värde kunde läsas.");
            }
        foreach (string option in validOptions)
            {
                if (string.Equals(option, input.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return option;
                }
            }
            Console.WriteLine($"Ogiltig inmatning. Välj mellan: {string.Join(", ", validOptions)}");
        }
    }
}