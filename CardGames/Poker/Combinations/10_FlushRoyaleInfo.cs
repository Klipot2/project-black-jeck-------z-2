namespace Casino.CardGames.Poker.Combinations
{
    public class FlushRoyaleInfo : CombinationInfo
    {
        public FlushRoyaleInfo(List<Card> cards) : base(cards) { }

        protected override bool IsCombinationPresent()
        {
            int fromAceToTenProduct = _valueComposition[Card.Value.Ace] 
                * _valueComposition[Card.Value.King] 
                * _valueComposition[Card.Value.Queen]
                * _valueComposition[Card.Value.Jack]
                * _valueComposition[Card.Value.Ten];

            return (fromAceToTenProduct != 0) && FlushInfo.IsFlush(_suitComposition);
        }
    }
}