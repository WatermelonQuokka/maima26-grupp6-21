
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
                            if (bool.TryParse(value, out bool result))
                            {
                                rules.DealerHitsOnSoft17 = result;
                            }
                            else
                            {
                                ConsoleUi.ShowRulesFormatError();
                            }
                        }
                       else if (key == "NumberOfDecks")
                        {
                            if (int.TryParse(value, out int decks) && decks > 0)
                            {
                                rules.NumberOfDecks = decks;
                            }
                            else
                            {
                                ConsoleUi.ShowRulesFormatError();
                                rules.NumberOfDecks = 1;
                            }
                        }
                        else if (key == "ReshuffleThreshold")
                        {
                            if (int.TryParse(value, out int threshold) && threshold > 0)
                            {
                                rules.ReshuffleThreshold = threshold;
                            }
                            else
                            {
                                ConsoleUi.ShowRulesFormatError();
                            }
                        }
                        else if (key == "NaturalBlackjackBeatsThreeCard21")
                        {
                            if (bool.TryParse(value, out bool result))
                            {
                                rules.NaturalBlackjackBeatsThreeCard21 = result;
                            }
                            else
                            {
                                ConsoleUi.ShowRulesFormatError();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)  
            {
                ConsoleUi.ShowRulesReadError(ex.Message);
            }

            return rules;
            }
        }
    }

       

   


