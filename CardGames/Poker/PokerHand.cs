using Casino.CardGames.Poker.Combinations;

namespace Casino.CardGames.Poker
{
    /// <summary>
    /// Represents a poker hand in a card game.
    /// </summary>
    public class PokerHand : Hand
    {
        private readonly ValueData _handValue;
        private int _bank = 0;
        private int _bet = 0;

        /// <summary>
        /// Gets the value data representing the hand's poker value.
        /// </summary>
        public ValueData Value { get { return _handValue; } }

        /// <summary>
        /// Gets or sets the bank amount associated with the hand.
        /// </summary>
        public int Bank 
        { 
            get { return _bank;} 
            set 
            {
                if (value < 0)
                    throw new ArgumentException("Bank cannot be negative!");
                if (value % FiveCardPoker.MIN_BET != 0)
                    throw new ArgumentException("Bank should be proportional to the minimum bet value.");
                _bank = value;
            }
        }

        /// <summary>
        /// Gets or sets the bet amount associated with the hand.
        /// </summary>
        public int Bet 
        { 
            get { return _bet;} 
            set 
            {
                if (value < 0)
                    throw new ArgumentException("Bet cannot be negative!");
                if (value % FiveCardPoker.MIN_BET != 0)
                    throw new ArgumentException("Bet should be proportional to the minimum bet value.");
                _bet = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the PokerHand class with the specified owner's name and hand size.
        /// </summary>
        /// <param name="ownerName">The name of the hand's owner.</param>
        /// <param name="handSize">The size of the hand.</param>
        public PokerHand(string ownerName, int handSize) : base(ownerName)
        {
            _handValue = new ValueData(handSize, true);
        }

        /// <summary>
        /// Initializes a new instance of the PokerHand class with the specified owner's name and initial cards.
        /// </summary>
        /// <param name="ownerName">The name of the hand's owner.</param>
        /// <param name="cards">Initial cards in the hand.</param>
        public PokerHand(string ownerName, List<Card> cards) : base(ownerName, cards)
        {
            _handValue = new ValueData(cards.Count, true);
            if (Size == 5) HandEvaluator.CalculateHandValueData(_hand, _handValue);
        }

        /// <summary>
        /// Adds a card to the hand and updates the hand value if the hand size is 5.
        /// </summary>
        /// <param name="card">The card to be added.</param>
        public override void AddCard(Card card)
        {
            base.AddCard(card);
            if (Size == 5) HandEvaluator.CalculateHandValueData(_hand, _handValue);
        }

        /// <summary>
        /// Adds a list of cards to the hand and updates the hand value if the hand size is 5.
        /// </summary>
        /// <param name="cards">The list of cards to be added.</param>
        public override void AddCards(List<Card> cards)
        {
            base.AddCards(cards);
            if (Size == 5) HandEvaluator.CalculateHandValueData(_hand, _handValue);
        }

        /// <summary>
        /// Swaps a card in the hand with a new card and updates the hand value if the hand size is 5.
        /// </summary>
        /// <param name="previousCardPosition">The position of the card to be replaced.</param>
        /// <param name="newCard">The new card to be placed in the hand.</param>
        public override void SwapCard(int previousCardPosition, Card newCard)
        {
            base.SwapCard(previousCardPosition, newCard);
            if (Size == 5) HandEvaluator.CalculateHandValueData(_hand, _handValue);
        }

        /// <summary>
        /// Swaps a card in the hand with a new card without updating the hand value immediately.
        /// </summary>
        /// <param name="previousCardPosition">The position of the card to be replaced.</param>
        /// <param name="newCard">The new card to be placed in the hand.</param>
        public void PlainSwapCard(int previousCardPosition, Card newCard)
        {
            base.SwapCard(previousCardPosition, newCard);
        }

        /// <summary>
        /// Forces an update of the hand value. Useful when the hand size is not 5.
        /// </summary>
        public void ForceUpdateHandValue()
        {
            if (Size == 5)
                HandEvaluator.CalculateHandValueData(_hand, _handValue);
            else
                _handValue.ResetData();
        }

        /// <summary>
        /// Resets the hand by clearing all cards and resetting the hand value.
        /// </summary>
        public override void ResetHand()
        {
            base.ResetHand();
            _handValue.ResetData();
        }

        /// <summary>
        /// Makes a bet with the specified amount, deducting it from the bank.
        /// </summary>
        /// <param name="amount">The amount to bet.</param>
        public void MakeBet(int amount)
        {
            Bet += amount;
            Bank -= amount;
        }
    }
}