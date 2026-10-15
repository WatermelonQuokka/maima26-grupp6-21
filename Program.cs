using Tjugoett.Cards;

// Skapa en ny hand för att testa
var hand = new Hand();

// Testfall 1: A + 6 (ska bli 17)
hand.AddCard(new Card(Suit.Hearts, Rank.Ace));
hand.AddCard(new Card(Suit.Hearts, Rank.Six));
Console.WriteLine($"A+6 = {hand.GetTotalValue()}"); 

// Testfall 2: A + 6 + 9 (ska bli 16)
hand.Clear();
hand.AddCard(new Card(Suit.Hearts, Rank.Ace));
hand.AddCard(new Card(Suit.Hearts, Rank.Six));
hand.AddCard(new Card(Suit.Hearts, Rank.Nine));
Console.WriteLine($"A+6+9 = {hand.GetTotalValue()}"); 

// Testfall 3: A + A + 9 (ska bli 21)
hand.Clear();
hand.AddCard(new Card(Suit.Hearts, Rank.Ace));
hand.AddCard(new Card(Suit.Diamonds, Rank.Ace));
hand.AddCard(new Card(Suit.Hearts, Rank.Nine));
Console.WriteLine($"A+A+9 = {hand.GetTotalValue()}"); 

// Testfall 4: A + A + A + A (ska bli 14)
hand.Clear();
hand.AddCard(new Card(Suit.Hearts, Rank.Ace));
hand.AddCard(new Card(Suit.Diamonds, Rank.Ace));
hand.AddCard(new Card(Suit.Clubs, Rank.Ace));
hand.AddCard(new Card(Suit.Spades, Rank.Ace));
Console.WriteLine($"A+A+A+A = {hand.GetTotalValue()}"); 
