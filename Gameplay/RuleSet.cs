using System;
using System.IO;
using TwentyOne.UI;

namespace TwentyOne.Gameplay
{
    public class RuleSet
    {
        // Settings for house rules
        public bool DealerHitsOnSoft17 { get; set; } = false;
        public int NumberOfDecks { get; set; } = 1;
        public int ReshuffleThreshold { get; set; } = 15;
        public bool NaturalBlackjackBeatsThreeCard21 { get; set; } = true;

        // Method to read rules from a file
        public static RuleSet LoadFromFile(string filePath)
        {
            RuleSet rules = new RuleSet();

            // Check if file exists, if not return default rules
            if (!File.Exists(filePath))
            {
                ConsoleUi.ShowRulesFileNotFound();
                return rules;
            }

            try
            {
                // Read all lines from the text file
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    // Split line into key and value by =
                    string[] parts = line.Split('=');
                    
                    if (parts.Length == 2)
                    {
                        string key = parts[0].Trim();
                        string value = parts[1].Trim();

                        // Set values based on key name
                        if (key == "DealerHitsOnSoft17")
                        {
                            rules.DealerHitsOnSoft17 = bool.Parse(value);
                        }
                        else if (key == "NumberOfDecks")
                        {
                            rules.NumberOfDecks = int.Parse(value);
                        }
                        else if (key == "ReshuffleThreshold")
                        {
                            rules.ReshuffleThreshold = int.Parse(value);
                        }
                        else if (key == "NaturalBlackjackBeatsThreeCard21")
                        {
                            rules.NaturalBlackjackBeatsThreeCard21 = bool.Parse(value);
                        }
                    }
                }
            }
            catch (FormatException ex)
            {
                // If error happens, notify user and use default rules
                ConsoleUi.ShowRulesFormatError(ex.Message);   
            }
            catch (Exception ex)
            {
                ConsoleUi.ShowRulesReadError(ex.Message);
            }

            return rules;
        }
    }
}

