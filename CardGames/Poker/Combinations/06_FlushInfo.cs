namespace Casino.CardGames.Poker.Combinations
{
    public class FlushInfo : CombinationInfo
    {
        public FlushInfo(List<Card> cards) : base(cards) { }

        protected override bool IsCombinationPresent() => IsFlush(_suitComposition);
        
        public static bool IsFlush(Dictionary<Card.Suit, int> suitComposition) => suitComposition.ContainsValue(5);
    }
}