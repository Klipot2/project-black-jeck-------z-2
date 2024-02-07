namespace Casino.CardGames.Poker.Combinations
{
    public class FlushInfo : CombinationInfo
    {
        public FlushInfo(List<Card> cards) : base(cards) { }

        protected override bool IsCombinationPresent() => IsFlush(_suitComposition);
        
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

        protected override void SortCards()
        {
            base.SortCards();

            List<Card> nonFlushCards = new();
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
            string.Format("Composition does not contain flush, so couldn't find one!"));
        }
    }
}