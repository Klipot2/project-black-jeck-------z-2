namespace Casino.CardGames.Poker.Combinations
{
    public class SetInfo : CombinationInfo
    {
        public SetInfo(List<Card> cards) : base(cards) { }

        protected override bool IsCombinationPresent() => CompositionContainsOneOf(new int[] { 3, 4 });

        protected override void SortCards()
        {
            base.SortCards();

            List<Card> set = PopDuplicates(_cards);
            InsertAtFront(set); 
        }       
    }
}