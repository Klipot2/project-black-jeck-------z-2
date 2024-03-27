namespace Casino.CardGames.Poker.Combinations
{
    public class FreeHandInfo : CombinationInfo
    {
        public FreeHandInfo(List<Card> cards) : base(cards){}

        protected override bool IsCombinationPresent() => true;
    }
}