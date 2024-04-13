namespace Casino.CardGames.Poker.Combinations
{
    /// <summary>
    /// Represents a hand with a set (three of a kind) in a poker hand.
    /// </summary>
    public class SetInfo : CombinationInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetInfo"/> class.
        /// </summary>
        /// <param name="cards">List of cards in the hand.</param>
        public SetInfo(List<Card> cards) : base(cards) { }

        /// <summary>
        /// Checks if the hand contains a set (three of a kind).
        /// </summary>
        /// <returns>True if a set is present, false otherwise.</returns>
        protected override bool IsCombinationPresent() => CompositionContainsOneOf([3, 4]);

        /// <summary>
        /// Sorts the cards based on their importance in a set combination.
        /// </summary>
        protected override void SortCards()
        {
            base.SortCards();

            List<Card> set = PopDuplicates(_cards);
            InsertAtFront(set);
        }
    }
}
