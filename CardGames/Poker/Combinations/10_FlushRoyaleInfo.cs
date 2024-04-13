namespace Casino.CardGames.Poker.Combinations
{
    /// <summary>
    /// Represents information about a Flush Royale combination in a poker hand.
    /// </summary>
    public class FlushRoyaleInfo : CombinationInfo
    {
        private List<Card>? _flushRoyale;

        /// <summary>
        /// Initializes a new instance of the FlushRoyaleInfo class with the specified list of cards.
        /// </summary>
        /// <param name="cards">The list of cards to analyze.</param>
        public FlushRoyaleInfo(List<Card> cards) : base(cards) { }

        /// <summary>
        /// Determines if the Flush Royale combination is present in the hand.
        /// </summary>
        /// <returns>True if the Flush Royale combination is present; otherwise, false.</returns>
        protected override bool IsCombinationPresent()
        {
            if (!StraightFlushInfo.TryGetStraighFlush(_cards, out _flushRoyale)) return false;

            List<Card.Value> flushRoyaleValues = [Card.Value.Ten, Card.Value.Jack, Card.Value.Queen, Card.Value.King, Card.Value.Ace];
           
            bool allCardsAreFlushRoyale = true;
            foreach (var card in _flushRoyale)
            {
                allCardsAreFlushRoyale = allCardsAreFlushRoyale && flushRoyaleValues.Contains(card.CardValue);
            }            

            return allCardsAreFlushRoyale;
        }

        /// <summary>
        /// Sorts the cards and moves the Flush Royale cards to the front of the list.
        /// </summary>
        protected override void SortCards()
        {
            base.SortCards();

            if (_flushRoyale != null)
            {
                foreach (var card in _flushRoyale)
                {
                    _cards.Remove(card);
                }
                InsertAtFront(_flushRoyale);
            }
        }
    }
}