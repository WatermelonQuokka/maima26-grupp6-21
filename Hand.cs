
using System.Collections.Generic;

namespace Tjugoett.Cards
{
    public class Hand
    {
        private readonly List<Card> _cards = new List<Card>();

        public void AddCard(Card card)
        {
            if (card != null)
            {
                _cards.Add(card);
            }
        }

        public IReadOnlyList<Card> GetCards()
        {
            return _cards.AsReadOnly();
        }

        public void Clear()
        {
            _cards.Clear();
        }

        public int GetTotalValue()
        {
            int total = 0;
            int aceCount = 0;

            foreach (var card in _cards)
            {
                switch (card.Rank)
                {
                    case Rank.Ace:
                        total += 11;
                        aceCount++;
                        break;
                    case Rank.Jack:
                    case Rank.Queen:
                    case Rank.King:
                        total += 10;
                        break;
                    default:
                        total += (int)card.Rank;
                        break;
                }
            }

            while (total > 21 && aceCount > 0)
            {
                total -= 10;
                aceCount--;
            }

            return total;
        }

        public bool IsBust()
        {
            return GetTotalValue() > 21;
        }
    }
}
