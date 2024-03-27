namespace Casino.CardGames.Poker.Combinations
{
    public class PairInfo : CombinationInfo
    {
        public PairInfo(List<Card> cards) : base(cards) { }

        protected override bool IsCombinationPresent() => CompositionContainsOneOf([2, 3, 4]);

        protected override void SortCards()
        {
            base.SortCards();

            List<Card> pair = PopDuplicates(_cards);
            InsertAtFront(pair); 
        } 
    }
}