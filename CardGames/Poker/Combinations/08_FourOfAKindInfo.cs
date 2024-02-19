namespace Casino.CardGames.Poker.Combinations
{
    /// <summary>
    /// Represents a hand with four of a kind in a poker hand.
    /// </summary>
    public class FourOfAKindInfo : CombinationInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FourOfAKindInfo"/> class.
        /// </summary>
        /// <param name="cards">List of cards in the hand.</param>
        public FourOfAKindInfo(List<Card> cards) : base(cards) { }

        /// <summary>
        /// Checks if the hand contains four of a kind.
        /// </summary>
        /// <returns>True if four of a kind is present, false otherwise.</returns>
        protected override bool IsCombinationPresent() => CompositionContainsOneOf(new int[] { 4 });

        /// <summary>
        /// Sorts the cards based on their importance in a four of a kind combination.
        /// </summary>
        protected override void SortCards()
        {
            base.SortCards();

            List<Card> fourOfAKind = PopDuplicates(_cards);
            InsertAtFront(fourOfAKind);
        }
    }
}
