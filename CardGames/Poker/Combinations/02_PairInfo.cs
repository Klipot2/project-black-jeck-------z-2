namespace Casino.CardGames.Poker.Combinations
{
    /// <summary>
    /// Represents a pair in a poker hand.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="PairInfo"/> class.
    /// </remarks>
    /// <param name="cards">List of cards in the hand.</param>
    public class PairInfo(List<Card> cards) : CombinationInfo(cards)
    {
        /// <summary>
        /// Checks if the hand contains a pair.
        /// </summary>
        /// <returns>True if a pair is present, false otherwise.</returns>
        protected override bool IsCombinationPresent() => CompositionContainsOneOf([2, 3, 4]);

        /// <summary>
        /// Sorts the cards based on their importance in a pair combination.
        /// </summary>
        protected override void SortCards()
        {
            base.SortCards();

            List<Card> pair = PopDuplicates(_cards);
            InsertAtFront(pair);
        }
    }
}
