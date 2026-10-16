using Tjugoett.Gameplay;

try
{
    Game game = new Game();
    game.Run();

   // Tjugoett.Cards.Hand naturalHand = new Tjugoett.Cards.Hand();
//naturalHand.AddCard(new Tjugoett.Cards.Card(Tjugoett.Cards.Suit.Hearts, Tjugoett.Cards.Rank.Ace));
//naturalHand.AddCard(new Tjugoett.Cards.Card(Tjugoett.Cards.Suit.Spades, Tjugoett.Cards.Rank.King));

//Console.WriteLine($"Test 1 - IsNatural: {naturalHand.IsNatural}"); // Ska skriva ut True

//Tjugoett.Cards.Hand threeCardHand = new Tjugoett.Cards.Hand();
//threeCardHand.AddCard(new Tjugoett.Cards.Card(Tjugoett.Cards.Suit.Hearts, Tjugoett.Cards.Rank.Seven));
//threeCardHand.AddCard(new Tjugoett.Cards.Card(Tjugoett.Cards.Suit.Diamonds, Tjugoett.Cards.Rank.Seven));
//threeCardHand.AddCard(new Tjugoett.Cards.Card(Tjugoett.Cards.Suit.Clubs, Tjugoett.Cards.Rank.Seven));

//Console.WriteLine($"Test 2 - Value: {threeCardHand.GetValue()}"); // Ska skriva ut 21
//Console.WriteLine($"Test 2 - IsNatural: {threeCardHand.IsNatural}"); // Ska skriva ut False
}
catch (EndOfStreamException)
{
    Console.WriteLine("Inmatningen avslutades. Hej då!");
}
