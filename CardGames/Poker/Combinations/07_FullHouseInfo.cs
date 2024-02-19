namespace Casino.CardGames.Poker.Combinations
{
    /// <summary>
    /// Represents a hand with a full house in a poker hand.
    /// </summary>
    public class FullHouseInfo : CombinationInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FullHouseInfo"/> class.
        /// </summary>
        /// <param name="cards">List of cards in the hand.</param>
        public FullHouseInfo(List<Card> cards) : base(cards) { }

        /// <summary>
        /// Checks if the hand contains a full house.
        /// </summary>
        /// <returns>True if a full house is present, false otherwise.</returns>
        protected override bool IsCombinationPresent()
        {
            bool hasPair = false;
            bool hasSet = false;
            foreach (var value in _valueComposition.Values)
            {
                if (value == 2) hasPair = true;
                if (value == 3) hasSet = true;
            }

            return hasSet && hasPair;
        }

        /// <summary>
        /// Sorts the cards based on their importance in a full house combination.
        /// </summary>
        protected override void SortCards()
        {
            base.SortCards();

            List<Card> set = PopDuplicates(_cards);
            List<Card> pair = PopDuplicates(_cards);
            InsertAtFront(pair);
            InsertAtFront(set);
        }
    }
}
