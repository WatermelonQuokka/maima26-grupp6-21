using System;
using System.IO;
using System.Text.Json;

namespace Tjugoett.Gameplay
{
    public class RuleSet
    {
        // Inställningar för regler med standardvärden
        public bool DealerHitsOnSoft17 { get; set; } = false;
        public int NumberOfDecks { get; set; } = 1;
        public int ReshuffleThreshold { get; set; } = 15;
        public bool NaturalBeatsMultiCard21 { get; set; } = true;

        // Läser regler från JSON-fil eller använder standardvärden om fel uppstår
        public static RuleSet LoadFromFile(string filePath = "rules.json")
        {
            RuleSet rules = new RuleSet();

            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    var loadedRules = JsonSerializer.Deserialize<RuleSet>(json);

                    if (loadedRules != null)
                    {
                        rules = loadedRules;
                    }
                }
                else
                {
                    Console.WriteLine("Info: Konfigurationsfil för regler saknas. Standardregler används.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Varning: Kunde inte läsa {filePath} ({ex.Message}). Standardregler används.");
            }

            return rules;
        }
    }
}
