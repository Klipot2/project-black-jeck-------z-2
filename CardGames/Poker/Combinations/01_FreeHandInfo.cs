namespace Casino.CardGames.Poker.Combinations
{
    /// <summary>
    /// Represents a free hand, which always indicates the presence of a hand.
    /// </summary>
    public class FreeHandInfo : CombinationInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FreeHandInfo"/> class.
        /// </summary>
        /// <param name="cards">List of cards in the hand.</param>
        public FreeHandInfo(List<Card> cards) : base(cards) { }

        /// <summary>
        /// Checks if the free hand combination is always present.
        /// </summary>
        /// <returns>Always returns true.</returns>
        protected override bool IsCombinationPresent() => true;
    }
}
