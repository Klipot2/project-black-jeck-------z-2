namespace Casino.CardGames.Poker.Combinations
{
    public class StraightFlushInfo : CombinationInfo
    {
        public StraightFlushInfo(List<Card> cards) : base(cards) { }

        protected override bool IsCombinationPresent()
            => StraightInfo.IsStraight(_valueComposition) && FlushInfo.IsFlush(_suitComposition);

        protected override void SortCards()
        {
            base.SortCards();

            if (_valueComposition[Card.Value.Ace] * _valueComposition[Card.Value.Two] != 0)
            {
                // May be a problem with ace being a reference.
                Card ace = _cards[0];
                _cards.RemoveAt(0);
                _cards.Add(ace);
            }
        }
    }
}