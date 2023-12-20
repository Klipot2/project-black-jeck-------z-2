using Casino.CardGames.Poker.Combinations;

namespace Casino.CardGames.Poker
{
    /// <summary>
    /// Base class for hand assessment. Classes extending <see cref="CombinationInfo"/> 
    /// should imply a certain poker combination that will be used as a base for assessment.
    /// </summary>
    public abstract class CombinationInfo
    {
        /// <summary> Describes if combination is present in hand. </summary>
        public bool CombinationPresent { get { return _isPresent; } }
        /// <summary> Represents combination value relative to other combinations of the same type. </summary>
        /// <seealso cref="ValueData"/>
        public ValueData CombinationValue { get { return _combinationValue; } }

        private readonly bool _isPresent;
        private readonly ValueData _combinationValue;

        //Keeping collections below is not optimized
        /// <summary> Contains list of all cards present in hand. </summary>
        protected readonly List<Card> _cards;
        /// <summary> Shows how many cards of each suit are currently in hand. </summary>
        protected readonly Dictionary<Card.Suit, int> _suitComposition;
        /// <summary> Shows how many cards of each value are currently in hand. </summary>
        protected readonly Dictionary<Card.Value, int> _valueComposition;
        
        /// <summary>
        /// Initializes a new instance of <see cref="CombinationInfo"/> class 
        /// based on <see cref="List{Card}"/> containing cards from assessed hand.
        /// </summary>
        /// <param name="cards">List of all cards present in assessed hand.</param>
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
            // Function potentially sorts initial hand.
            SortCards();
            _combinationValue = GenerateValueData();
        }

        /// <summary>
        /// Checks if combination is present, depending on a class that extends
        /// base <see cref="CombinationInfo"/>.
        /// </summary>
        /// <returns>
        /// A boolean representing combination presence in hand.
        /// This value is stored in <see cref="CombinationPresent"/>
        /// and doesn't change after <see cref="CombinationInfo"/> initialization.
        /// </returns>
        protected abstract bool IsCombinationPresent();
        /// <summary>
        /// Sorts cards based on their importance in combination. Combination is picked
        /// depending on a class that extends base <see cref="CombinationInfo"/>.
        /// </summary>
        /// <remarks>
        /// Runs only on <see cref="CombinationInfo"/> initialization.
        /// Always run <c>base.SortCards()</c> first when overriding.
        /// </remarks>
        protected virtual void SortCards()
        {
            HandEvaluator.SortCardsDescending(_cards);
            if (!IsCombinationPresent()) return;
        }

        private ValueData GenerateValueData()
        {
            // 0203040514
            // 0304050607
            // 0000000000000000000010101010030000000000000000000000000000001010101003000000000010101010031010101003
            ValueData combinationValue = new();
            if (!_isPresent) return combinationValue;

            foreach (var card in _cards)
            {
                int cardValue = HandEvaluator.ValueCard(card);
                combinationValue.AddValue(cardValue);
            }
            return combinationValue;
        }

        /// <summary>
        /// Pops most common duplicates from target <see cref="List{Card}"/>.
        /// </summary>
        /// <remarks>
        /// <see cref="List{Card}"/> needs to be sorted first.
        /// Duplicates are cards with equal <see cref="Card.Value"/>.
        /// </remarks>
        /// <param name="cards">List of cards that is checked for duplicates.</param>
        /// <returns>
        /// <see cref="List{Card}"/> that contains most common duplicates in no specified order.
        /// </returns>
        /// <exception cref="NullReferenceException">
        /// Thrown when there are no duplicates in the provided <see cref="List{Card}"/>.
        /// </exception>
        protected List<Card> PopDuplicates(List<Card> cards)
        {
            List<Card> duplicates = new();
            Card.Value? duplicateCard = null;
            int maxAmountOfDuplicates = 0;

            foreach (var cardValueAndAmountPair in _valueComposition)
            {
                // Will not work in out of order Dictionary
                if (cardValueAndAmountPair.Value >= maxAmountOfDuplicates)
                {
                    maxAmountOfDuplicates = cardValueAndAmountPair.Value;
                    duplicateCard = cardValueAndAmountPair.Key;
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

        /// <summary>
        /// Inserts cards in front of a sorted <see cref="_cards"/> list.
        /// </summary>
        /// <param name="cards">
        /// <see cref="List{Card}"/> containing cards which should be inserted in front.
        /// </param>
        protected void InsertAtFront(List<Card> cards)
        {
            foreach (var card in cards)
            {
                _cards.Insert(0, card);
            }
        }

        /// <summary>
        /// Checks if <see cref="_valueComposition"/> contains a certain value. 
        /// That corresponds to a certain amount of cards with the same value present in hand.
        /// </summary>
        /// <param name="values">
        /// An <see cref="Array"/> of values, any of which should be present for function to return <c>true</c>.
        /// </param>
        /// <returns>
        /// A boolean representing if a certain amount of cards with the same value are present in hand.
        /// </returns>
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