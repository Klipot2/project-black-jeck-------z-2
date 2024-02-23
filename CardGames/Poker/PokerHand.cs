using Casino.CardGames.Poker.Combinations;

namespace Casino.CardGames.Poker
{
    public class PokerHand : Hand
    {
        public ValueData Value { get { return _handValue; } }
        public int Bank 
        { 
            get { return _bank;} 
            set 
            {
                if (value < 0)
                    throw new ArgumentException("Person's bank cannot be negative!");
                if (value % FiveCardPoker.MIN_BET != 0)
                    throw new ArgumentException("Person's bank should be proportional to minimum bet value.");
                _bank = value;
            }
        }

        public int Bet 
        { 
            get { return _bet;} 
            set 
            {
                if (value < 0)
                    throw new ArgumentException("Person's bet cannot be negative!");
                if (value % FiveCardPoker.MIN_BET != 0)
                    throw new ArgumentException("Person's bet should be proportional to minimum bet value.");
                _bet = value;
            }
        }

        private readonly ValueData _handValue;
        private int _bank = 0;
        private int _bet = 0;

        public PokerHand(string ownerName, int handSize) : base(ownerName)
        {
            _handValue = new ValueData(handSize, true);
        }

        public PokerHand(string ownerName, List<Card> cards) : base(ownerName, cards)
        {
            _handValue = new ValueData(cards.Count, true);
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

        public void PlainSwapCard(int previousCardPosition, Card newCard)
        {
            base.SwapCard(previousCardPosition, newCard);
        }

        public void ForceUpdateHandValue()
        {
            if (Size == 5)
                HandEvaluator.CalculateHandValueData(_hand, _handValue);
            else
                _handValue.ResetData();
        }

        public override void ResetHand()
        {
            base.ResetHand();
            _handValue.ResetData();
        }

        public void MakeBet(int amount)
        {
            Bet += amount;
            Bank -= amount;
        }
    }
}