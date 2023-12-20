using System.Reflection.Metadata;
using Casino.CardGames.Poker.Combinations;

namespace Casino.CardGames.Poker
{
    public class PokerHand : Hand
    {
        private ValueData _handValue;

        public PokerHand(string ownerName, int handSize) : base(ownerName) 
        {
            _handValue = new ValueData(handSize * HandEvaluator.COMBINATION_AMOUNT);
        }

        public PokerHand(string ownerName, List<Card> cards) : base(ownerName, cards)
        { 
            _handValue = new ValueData(cards.Count * HandEvaluator.COMBINATION_AMOUNT);
            // Evaluate hand
        }
    }
}