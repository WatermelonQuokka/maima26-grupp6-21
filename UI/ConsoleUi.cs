using Tjugoett.Cards;
using Tjugoett.Graphics;
using Tjugoett.Participants;

namespace Tjugoett.UI;

public static class ConsoleUi
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
public static void ShowStatistics(List<Player> players)
    {
    Console.WriteLine("\n--- SPELSTATISTIK ---");
    Console.WriteLine("Namn\tVinster\tFörluster\tLika");

    foreach (Player player in players)
        {
            Console.WriteLine($"{player.Name}\t{player.Wins}\t{player.Losses}\t{player.Pushes}");
        }
        Console.WriteLine();
    }
        public static void ShowCards(IEnumerable<Card> cards, bool hideSecondCard = false)
        {
              List<Card> cardList = cards.ToList();
           List<string[]> renderCards = new List<string[]>();

           for (int i = 0; i < cardList.Count; i++)
        {
            bool faceUp = !(hideSecondCard && i == 1);
            renderCards.Add(CardRenderer.RenderCard(cardList[i], faceUp));
        }

        for (int lineIndex = 0; lineIndex < renderCards[0].Length; lineIndex++)
        {
            for (int i = 0; i < cardList.Count; i++)
            {
                Card card = cardList[i];
                bool faceUp = !(hideSecondCard && i == 1);
                string piece = renderCards[i][lineIndex];

                if (faceUp && IsRed(card))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                Console.Write(piece);
                Console.ResetColor();
                Console.Write("  ");
            }
            Console.WriteLine();
        }
    }
    private static bool IsRed(Card card) => card.Suit is Suit.Hearts or Suit.Diamonds;

    public static void ShowHand(Participant participant, bool hideSecondCard = false)
    {
        Console.WriteLine($"{participant.Name}s hand:");
        ShowCards(participant.Hand.GetCards(), hideSecondCard);

        if (!hideSecondCard || participant is not Dealer)
        {
            string softText = participant.Hand.IsSoft() ? " (mjuk)" : "";
            Console.WriteLine($"Poäng: {participant.Hand.GetTotalValue()}{softText}");
        }
        Console.WriteLine();
    }
}