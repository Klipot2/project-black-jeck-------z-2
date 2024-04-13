namespace Casino.CardGames.Poker.Combinations
{
    /// <summary>
    /// Represents a hand with four of a kind in a poker hand.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="FourOfAKindInfo"/> class.
    /// </remarks>
    /// <param name="cards">List of cards in the hand.</param>
    public class FourOfAKindInfo(List<Card> cards) : CombinationInfo(cards)
    {
        /// <summary>
        /// Checks if the hand contains four of a kind.
        /// </summary>
        /// <returns>True if four of a kind is present, false otherwise.</returns>
        protected override bool IsCombinationPresent() => CompositionContainsOneOf([4]);

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
