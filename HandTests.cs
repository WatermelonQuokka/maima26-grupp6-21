using Xunit;
using Tjugoett.Cards;

namespace Tjugoett.Tests
{
    public class HandTests
    {
        [Fact]
        public void Test_HandValue_Examples()
        {
            // A + 6 = 17
            var hand1 = new Hand();
            hand1.AddCard(new Card(Suit.Hearts, Rank.Ace));
            hand1.AddCard(new Card(Suit.Hearts, Rank.Six));
            Assert.Equal(17, hand1.GetTotalValue());

            // A + 6 + 9 = 16
            var hand2 = new Hand();
            hand2.AddCard(new Card(Suit.Hearts, Rank.Ace));
            hand2.AddCard(new Card(Suit.Hearts, Rank.Six));
            hand2.AddCard(new Card(Suit.Hearts, Rank.Nine));
            Assert.Equal(16, hand2.GetTotalValue());

            // A + A + 9 = 21
            var hand3 = new Hand();
            hand3.AddCard(new Card(Suit.Hearts, Rank.Ace));
            hand3.AddCard(new Card(Suit.Spades, Rank.Ace));
            hand3.AddCard(new Card(Suit.Hearts, Rank.Nine));
            Assert.Equal(21, hand3.GetTotalValue());

            // A + A + A + A = 14
            var hand4 = new Hand();
            hand4.AddCard(new Card(Suit.Hearts, Rank.Ace));
            hand4.AddCard(new Card(Suit.Spades, Rank.Ace));
            hand4.AddCard(new Card(Suit.Clubs, Rank.Ace));
            hand4.AddCard(new Card(Suit.Diamonds, Rank.Ace));
            Assert.Equal(14, hand4.GetTotalValue());
        }
    }
}
