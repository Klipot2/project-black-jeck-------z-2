namespace Casino.CardGames.Poker.Combinations
{
    /// <summary>
    /// Represents a hand with a straight in a poker hand.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="StraightInfo"/> class.
    /// </remarks>
    /// <param name="cards">List of cards in the hand.</param>
    public class StraightInfo(List<Card> cards) : CombinationInfo(cards)
    {
        /// <summary>
        /// Checks if the given card values represent a straight.
        /// </summary>
        /// <param name="valueComposition">Value composition of the hand.</param>
        /// <returns>True if the hand has a straight, false otherwise.</returns>
        public static bool IsStraight(Dictionary<Card.Value, int> valueComposition)
        {
            int fromAceToFiveProduct = valueComposition[Card.Value.Ace]
                * valueComposition[Card.Value.Two]
                * valueComposition[Card.Value.Three]
                * valueComposition[Card.Value.Four]
                * valueComposition[Card.Value.Five];
            if (fromAceToFiveProduct != 0) return true;

            string valuesAsString = "";
            foreach (var value in valueComposition.Values)
            {
                if (value > 0) valuesAsString += "1";
                else valuesAsString += "0";
            }

            return valuesAsString.Contains("11111");
        }

        /// <summary>
        /// Removes the straight cards from the given list of cards.
        /// </summary>
        /// <param name="cards">List of cards to remove the straight from.</param>
        /// <returns>List of cards forming the straight.</returns>
        public static List<Card> PopStraightFromCards(List<Card> cards)
        {
            Dictionary<Card.Value, int> valueComposition = GenerateValueComposition(cards);
            List<Card.Value> straightValueComposition = GetStraightComposition(valueComposition);
            List<Card> straightCards = PopCardsFromComposition(cards, straightValueComposition);
            return straightCards;
        }

        /// <summary>
        /// Checks if the hand contains a straight.
        /// </summary>
        /// <returns>True if a straight is present, false otherwise.</returns>
        protected override bool IsCombinationPresent() => IsStraight(_valueComposition);

        /// <summary>
        /// Sorts the cards based on their importance in a straight combination.
        /// </summary>
        protected override void SortCards()
        {
            base.SortCards();

            List<Card.Value> straightComposition = GetStraightComposition(_valueComposition);
            List<Card> straight = PopCardsFromComposition(_cards, straightComposition);
            InsertAtFront(straight);
        }

        private static List<Card.Value> GetStraightComposition(Dictionary<Card.Value, int> valueComposition)
        {
            List<Card.Value> potentialStraight = [];
            List<Card.Value> confirmedStraight = [];

            if (valueComposition[Card.Value.Ace] > 0)
            {
                potentialStraight.Add(Card.Value.Ace);
            }

            foreach (var cardValueAndAmountPair in valueComposition)
            {
                if (cardValueAndAmountPair.Value > 0)
                {
                    potentialStraight.Add(cardValueAndAmountPair.Key);
                    if (potentialStraight.Count > 5)
                    {
                        potentialStraight.RemoveAt(0);
                    }
                    if (potentialStraight.Count == 5)
                    {
                        confirmedStraight = new List<Card.Value>(potentialStraight);
                    }
                }
                else
                {
                    potentialStraight.Clear();
                }
            }

            if (confirmedStraight.Count == 0)
                throw new ArgumentNullException(
                $"The given value composition {valueComposition} does not contain a straight.");

            return confirmedStraight;
        }
    }
}
