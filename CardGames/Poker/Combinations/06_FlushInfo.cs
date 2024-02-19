namespace Casino.CardGames.Poker.Combinations
{
    /// <summary>
    /// Represents a hand with a flush in a poker hand.
    /// </summary>
    public class FlushInfo : CombinationInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlushInfo"/> class.
        /// </summary>
        /// <param name="cards">List of cards in the hand.</param>
        public FlushInfo(List<Card> cards) : base(cards) { }

        /// <summary>
        /// Checks if the hand contains a flush.
        /// </summary>
        /// <returns>True if a flush is present, false otherwise.</returns>
        protected override bool IsCombinationPresent() => IsFlush(_suitComposition);

        /// <summary>
        /// Checks if the given suit composition represents a flush.
        /// </summary>
        /// <param name="suitComposition">Suit composition of the hand.</param>
        /// <returns>True if the hand has a flush, false otherwise.</returns>
        public static bool IsFlush(Dictionary<Card.Suit, int> suitComposition)
        {
            foreach (var pair in suitComposition)
            {
                if (pair.Value >= 5)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Sorts the cards based on their importance in a flush combination.
        /// </summary>
        protected override void SortCards()
        {
            base.SortCards();

            List<Card> nonFlushCards = new List<Card>();
            Card.Suit flushSuit = GetFlushSuit(_suitComposition);

            foreach (var card in _cards)
            {
                if (card.CardSuit != flushSuit)
                {
                    nonFlushCards.Add(card);
                }
            }

            foreach (var card in nonFlushCards)
            {
                _cards.Remove(card);
            }

            _cards.AddRange(nonFlushCards);
        }

        /// <summary>
        /// Gets the suit that forms the flush from the given suit composition.
        /// </summary>
        /// <param name="suitComposition">Suit composition of the hand.</param>
        /// <returns>Suit forming the flush.</returns>
        public static Card.Suit GetFlushSuit(Dictionary<Card.Suit, int> suitComposition)
        {
            foreach (var pair in suitComposition)
            {
                if (pair.Value >= 5)
                {
                    return pair.Key;
                }
            }

            throw new ArgumentNullException(
            "The suit composition does not contain a flush, so couldn't find one!");
        }
    }
}
```
