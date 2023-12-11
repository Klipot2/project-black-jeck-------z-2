using Casino.CardGames.Poker.Combinations;

namespace Casino.CardGames.Poker
{
    public abstract class CombinationInfo
    {
        public bool CombinationPresent { get { return _isPresent; } }
        public ValueData CombinationValue { get { return _combinationValue; } }

        private readonly bool _isPresent;
        private readonly ValueData _combinationValue;

        //Keeping collections below is not optimized
        protected readonly List<Card> _cards;
        protected readonly Dictionary<Card.Suit, int> _suitComposition;
        protected readonly Dictionary<Card.Value, int> _valueComposition;

        public CombinationInfo(List<Card> cards)
        {
            _cards = cards;

            _suitComposition = new();
            foreach (Card.Suit suit in Enum.GetValues(typeof(Card.Suit)))
            {
                _suitComposition[suit] = 0;
            }

            _valueComposition = new();
            foreach (Card.Value value in Enum.GetValues(typeof(Card.Value)))
            {
                _valueComposition[value] = 0;
            }

            foreach (var card in cards)
            {
                _suitComposition[card.CardSuit] += 1;
                _valueComposition[card.CardValue] += 1;
            }

            _isPresent = IsCombinationPresent();
            SortCards();
            _combinationValue = GenerateValueData();
        }

        protected abstract bool IsCombinationPresent();
        protected virtual void SortCards()
        {
            HandEvaluator.SortCardsDescending(_cards);
            if (!IsCombinationPresent()) return;
        }

        private ValueData GenerateValueData()
        {
            ValueData combinationValue = new();
            if (!_isPresent) return combinationValue;

            foreach (var card in _cards)
            {
                int cardValue = HandEvaluator.ValueCard(card);
                combinationValue.AddValue(cardValue);
            }
            return combinationValue;
        }

        // TODO: Refactor PopDuplicates to pop most common duplicate first (no int required)
        /// <summary>
        /// Pops most common duplicates from target <c>List&lt;Card&gt;</c>.
        /// </summary>
        /// <remarks>
        /// <c>List&lt;Card&gt;</c> needs to be sorted first.<br/>
        /// Duplicates are cards with equal <c>Card.Value</c>.
        /// </remarks>
        /// <param name="cards">list of cards that is checked for duplicates.</param>
        /// <returns>
        /// <c>List&lt;Card&gt;</c> that contains most common duplicates in no specified order.
        /// </returns>
        /// <exception cref="NullReferenceException">
        /// Thrown when there are no duplicates in the provided <c>List&lt;Card&gt;</c>.
        /// </exception>
        protected List<Card> PopDuplicates(List<Card> cards)
        {
            int expectedAmount = -1; // Placeholder
            List<Card> duplicates = new();
            Card.Value? duplicateCard = null;
            foreach (var cardValueAndAmountPair in _valueComposition)
            {
                if (cardValueAndAmountPair.Value == expectedAmount)
                {
                    duplicateCard = cardValueAndAmountPair.Key;
                    break;
                }
            }
            if (duplicateCard == null) throw new NullReferenceException(
                "FindDuplicates should only run if duplicates exist in provided list.");

            foreach (var card in cards)
            {
                if (card.CardValue == duplicateCard)
                {
                    duplicates.Add(card);
                    cards.Remove(card);
                }
            }

            return duplicates;
        }

        protected void InsertAtFront(List<Card> cards)
        {
            foreach (var card in cards)
            {
                _cards.Insert(0, card);
            }
        }

        protected bool CompositionContainsOneOf(int[] values)
        {
            bool doesContain = false;
            foreach (var value in values)
            {
                doesContain = doesContain || _valueComposition.ContainsValue(value);
            }
            return doesContain;
        }
    }
}