using Casino.CardGames.Poker.Combinations;

namespace Casino.CardGames.Poker
{
    public class PokerHand : Hand
    {
        public ValueData Value { get { return _handValue; } }

        private ValueData _handValue;

        public PokerHand(string ownerName, int handSize) : base(ownerName)
        {
            _handValue = new ValueData(handSize * HandEvaluator.COMBINATION_AMOUNT);
        }

        public PokerHand(string ownerName, List<Card> cards) : base(ownerName, cards)
        {
            _handValue = new ValueData(cards.Count * HandEvaluator.COMBINATION_AMOUNT);
            if (Size == 5) HandEvaluator.CalculateHandValueData(_hand, _handValue);
        }

        public override void AddCard(Card card)
        {
            base.AddCard(card);
            if (Size == 5) HandEvaluator.CalculateHandValueData(_hand, _handValue); 
        }

        public override void AddCards(List<Card> cards)
        {
            foreach (var card in cards)
            {
                base.AddCard(card);
            }
            if (Size == 5) HandEvaluator.CalculateHandValueData(_hand, _handValue); 
        }

        public override void SwapCard(int previousCardPosition, Card newCard)
        {
            base.SwapCard(previousCardPosition, newCard);
            if (Size == 5) HandEvaluator.CalculateHandValueData(_hand, _handValue);
        }

        public override void ResetHand()
        {
            base.ResetHand();
            _handValue.ResetData();
        }
    }
}