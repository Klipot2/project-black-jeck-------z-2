namespace Casino.CardGames.Poker.Combinations
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

        //Keeping collections below is not optimized
        /// <summary> Contains list of all cards present in hand. </summary>
        protected readonly List<Card> _cards;
        /// <summary> Shows how many cards of each suit are currently in hand. </summary>
        protected readonly Dictionary<Card.Suit, int> _suitComposition;
        /// <summary> Shows how many cards of each value are currently in hand. </summary>
        protected readonly Dictionary<Card.Value, int> _valueComposition;

        private readonly bool _isPresent;
        private readonly ValueData _combinationValue;

        /// <summary>
        /// Initializes a new instance of <see cref="CombinationInfo"/> class 
        /// based on <see cref="List{Card}"/> containing cards from assessed hand.
        /// </summary>
        /// <param name="cards">List of all cards present in assessed hand.</param>
        public CombinationInfo(List<Card> cards)
        {
            _cards = cards;
            _suitComposition = GenerateSuitComposition(cards);
            _valueComposition = GenerateValueComposition(cards);

            _isPresent = IsCombinationPresent();
            if (_isPresent) SortCards();
            _combinationValue = GenerateValueData();
        }

        /// <summary>
        /// Generates a dictionary representing the composition of card suits in the provided list of cards.
        /// </summary>
        /// <param name="cards">The list of cards to analyze.</param>
        /// <returns>A dictionary containing the count of each card suit present in the list.</returns>
        protected static Dictionary<Card.Suit, int> GenerateSuitComposition(List<Card> cards)
        {
            Dictionary<Card.Suit, int> suitComposition = [];
            foreach (Card.Suit suit in Enum.GetValues(typeof(Card.Suit)))
            {
                suitComposition[suit] = 0;
            }

            foreach (var card in cards)
            {
                suitComposition[card.CardSuit] += 1;
            }
            return suitComposition;
        }

        /// <summary>
        /// Generates a dictionary representing the composition of card values in the provided list of cards.
        /// </summary>
        /// <param name="cards">The list of cards to analyze.</param>
        /// <returns>A dictionary containing the count of each card value present in the list.</returns>
        protected static Dictionary<Card.Value, int> GenerateValueComposition(List<Card> cards)
        {
            Dictionary<Card.Value, int> valueComposition = [];
            foreach (Card.Value value in Enum.GetValues(typeof(Card.Value)))
            {
                valueComposition[value] = 0;
            }

            foreach (var card in cards)
            {
                valueComposition[card.CardValue] += 1;
            }
            return valueComposition;
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
            List<Card> duplicates = [];
            Card.Value? duplicateCardValue = null;
            int maxAmountOfDuplicates = 0;

            foreach (var cardValueAndAmountPair in _valueComposition)
            {
                // Will not work in out of order Dictionary (problem has yet to appear)
                if (cardValueAndAmountPair.Value >= maxAmountOfDuplicates && CardsContainValue(cards, cardValueAndAmountPair.Key))
                {
                    maxAmountOfDuplicates = cardValueAndAmountPair.Value;
                    duplicateCardValue = cardValueAndAmountPair.Key;
                }
            }
            if (duplicateCardValue == null) throw new NullReferenceException(
                "FindDuplicates should only run if duplicates exist in provided list.");

            for (int i = 0; i < maxAmountOfDuplicates; i++)
            {
                Card duplicateCard = PopCardOfValue(cards, duplicateCardValue ?? Card.Value.Two);
                duplicates.Add(duplicateCard);
            }

            return duplicates;
        }
        
        /// <summary>
        /// Removes and returns the first card with the specified value from the provided list of cards.
        /// </summary>
        /// <param name="cards">The list of cards to search for the target value.</param>
        /// <param name="targetValue">The value of the card to be removed.</param>
        /// <returns>The first card with the specified value removed from the list.</returns>
        protected static Card PopCardOfValue(List<Card> cards, Card.Value targetValue)
        {
            Card firstCardOfTargetValue = GetCardOfValue(cards, targetValue);
            cards.Remove(firstCardOfTargetValue);
            return firstCardOfTargetValue;
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

        /// <summary>
        /// Pops cards from the given list based on the provided value composition.
        /// </summary>
        /// <param name="cards">The list of cards to pop from.</param>
        /// <param name="valueComposition">The list of card values to pop from the cards list.</param>
        /// <returns>The list of cards popped from the original list based on the provided value composition.</returns>

        protected static List<Card> PopCardsFromComposition(List<Card> cards, List<Card.Value> valueComposition)
        {
            List<Card> cardsFromComposition = [];

            foreach (var cardValue in valueComposition)
            {
                Card compositionCard = PopCardOfValue(cards, cardValue);
                cardsFromComposition.Add(compositionCard);
            }

            return cardsFromComposition;
        }

        private ValueData GenerateValueData()
        {
            ValueData combinationValue = new(_cards.Count);
            if (!_isPresent) return combinationValue;

            foreach (var card in _cards)
            {
                int cardValue = HandEvaluator.ValueCard(card);
                combinationValue.AddValue(cardValue);
            }
            return combinationValue;
        }

        private static Card GetCardOfValue(List<Card> cards, Card.Value targetValue)
        {
            Card? firstCardOfTargetValue = null;

            foreach (var card in cards)
            {
                if (card.CardValue == targetValue)
                {
                    firstCardOfTargetValue = card;
                    break;
                }
            }

            if (firstCardOfTargetValue == null)
            {
                CardRenderer.PrintCards(cards);

                throw new ArgumentNullException(
                string.Format("{0} is null, because no cards of value {1} were found in {2}.",
                firstCardOfTargetValue, targetValue, cards));
            }

            return firstCardOfTargetValue;
        }

        private static bool CardsContainValue(List<Card> cards, Card.Value value)
        {
            foreach (var card in cards)
            {
                if (card.CardValue == value)
                    return true;
            }
            return false;
        }
    }
}