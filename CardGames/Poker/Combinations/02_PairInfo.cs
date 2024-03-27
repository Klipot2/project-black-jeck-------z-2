namespace Casino.CardGames.Poker.Combinations
{
    /// <summary>
    /// Represents a pair in a poker hand.
    /// </summary>
    public class PairInfo : CombinationInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PairInfo"/> class.
        /// </summary>
        /// <param name="cards">List of cards in the hand.</param>
        public PairInfo(List<Card> cards) : base(cards) { }

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
