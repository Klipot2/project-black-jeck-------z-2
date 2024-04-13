namespace Casino.CardGames.Poker.Combinations
{
    /// <summary>
    /// Represents information about a Straight Flush combination in a poker hand.
    /// </summary>
    public class StraightFlushInfo : CombinationInfo
    {
        private List<Card> _straightFlush = new List<Card>();

        /// <summary>
        /// Initializes a new instance of the StraightFlushInfo class with the specified list of cards.
        /// </summary>
        /// <param name="cards">The list of cards to analyze.</param>
        public StraightFlushInfo(List<Card> cards) : base(cards) { }

        /// <summary>
        /// Determines if the Straight Flush combination is present in the hand.
        /// </summary>
        /// <returns>True if the Straight Flush combination is present; otherwise, false.</returns>
        protected override bool IsCombinationPresent() => TryGetStraighFlush(_cards, out _straightFlush);

        /// <summary>
        /// Sorts the cards and moves the Straight Flush cards to the front of the list.
        /// </summary>
        protected override void SortCards()
        {
            base.SortCards();

            foreach (var card in _straightFlush)
            {
                _cards.Remove(card);
            }
            InsertAtFront(_straightFlush);
        }

        /// <summary>
        /// Attempts to extract the Straight Flush combination from the given list of cards.
        /// </summary>
        /// <param name="cards">The list of cards to analyze.</param>
        /// <param name="straightFlush">The list containing the Straight Flush combination, if found.</param>
        /// <returns>True if the Straight Flush combination is found; otherwise, false.</returns>
        public static bool TryGetStraighFlush(List<Card> cards, out List<Card> straightFlush)
        {
            straightFlush = new List<Card>();

            Dictionary<Card.Suit, int> suitComposition = GenerateSuitComposition(cards);
            if (!FlushInfo.IsFlush(suitComposition)) return false;

            Card.Suit flushSuit = FlushInfo.GetFlushSuit(suitComposition);
            List<Card> straightFlushCandidates = new List<Card>();
            foreach (var card in cards)
            {
                if (card.CardSuit == flushSuit)
                {
                    straightFlushCandidates.Add(card);
                }
            }

            Dictionary<Card.Value, int> valueComposition = GenerateValueComposition(straightFlushCandidates);
            if (!StraightInfo.IsStraight(valueComposition)) return false;
            straightFlush = StraightInfo.PopStraightFromCards(straightFlushCandidates);

            return true;
        }
    }
}