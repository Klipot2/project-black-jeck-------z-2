namespace Casino.CardGames.Poker.Combinations
{
    public class FlushRoyaleInfo : CombinationInfo
    {
        private List<Card> _flushRoyale;

        public FlushRoyaleInfo(List<Card> cards) : base(cards) { }

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

        protected override void SortCards()
        {
            base.SortCards();

            foreach (var card in _flushRoyale)
            {
                _cards.Remove(card);
            }
            InsertAtFront(_flushRoyale);
        }
    }
}