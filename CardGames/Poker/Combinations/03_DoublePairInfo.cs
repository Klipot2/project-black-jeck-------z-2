namespace Casino.CardGames.Poker.Combinations
{
    /// <summary>
    /// Represents a hand with two pairs in a poker hand.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="DoublePairInfo"/> class.
    /// </remarks>
    /// <param name="cards">List of cards in the hand.</param>
    public class DoublePairInfo(List<Card> cards) : CombinationInfo(cards)
    {

        /// <summary>
        /// Checks if the hand contains two pairs.
        /// </summary>
        /// <returns>True if two pairs are present, false otherwise.</returns>
        protected override bool IsCombinationPresent()
        {
            int pairAmount = 0;
            foreach (var value in _valueComposition.Values)
            {
                if (value >= 2) pairAmount += 1;
            }

            return pairAmount > 1;
        }

        /// <summary>
        /// Sorts the cards based on their importance in a two-pair combination.
        /// </summary>
        protected override void SortCards()
        {
            base.SortCards();

            List<Card> firstPair = PopDuplicates(_cards);
            List<Card> secondPair = PopDuplicates(_cards);
            InsertAtFront(secondPair);
            InsertAtFront(firstPair);
        }
    }
}
