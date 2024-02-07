namespace Casino.CardGames.Poker.Combinations
{
    public class StraightFlushInfo : CombinationInfo
    {
        private List<Card> _straightFlush = [];

        public StraightFlushInfo(List<Card> cards) : base(cards) { }

        protected override bool IsCombinationPresent() => TryGetStraighFlush(_cards, out _straightFlush);

        protected override void SortCards()
        {
            base.SortCards();

            foreach (var card in _straightFlush)
            {
                _cards.Remove(card);
            }
            InsertAtFront(_straightFlush);
        }

        public static bool TryGetStraighFlush(List<Card> cards, out List<Card> straightFlush)
        {
            straightFlush = [];

            Dictionary<Card.Suit, int> suitComposition = GenerateSuitComposition(cards);
            if (!FlushInfo.IsFlush(suitComposition)) return false;

            Card.Suit flushSuit = FlushInfo.GetFlushSuit(suitComposition);
            List<Card> straightFlushCandidates = [];
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