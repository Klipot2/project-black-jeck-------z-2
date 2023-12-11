namespace Casino.CardGames.Poker.Combinations
{
    public class PairInfo : CombinationInfo
    {
        public PairInfo(List<Card> cards) : base(cards) { }

        protected override bool IsCombinationPresent() => CompositionContainsOneOf(new int[] { 2, 3, 4 });

        protected override void SortCards()
        {
            base.SortCards(); // Potential bug with return from base function not working properly

            List<Card> pair = PopDuplicates(_cards);
            InsertAtFront(pair); 
        } 
    }
}