namespace Casino.CardGames.Poker.Combinations
{
    /// <summary>
    /// Represents a free hand, which always indicates the presence of a hand.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="FreeHandInfo"/> class.
    /// </remarks>
    /// <param name="cards">List of cards in the hand.</param>
    public class FreeHandInfo(List<Card> cards) : CombinationInfo(cards)
    {
        /// <summary>
        /// Checks if the free hand combination is always present.
        /// </summary>
        /// <returns>Always returns true.</returns>
        protected override bool IsCombinationPresent() => true;
    }
}
