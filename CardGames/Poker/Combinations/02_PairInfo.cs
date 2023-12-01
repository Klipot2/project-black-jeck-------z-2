namespace Casino.CardGames.Poker.Combinations
{
    public class PairInfo : CombinationInfo
    {
        public PairInfo(List<Card> cards) : base(cards) { }

        protected override bool IsCombinationPresent() => CompositionContainsOneOf(new int[] { 2, 3, 4 });

        protected override void SortCards()
        {
            base.SortCards();
            //Move those lines below to base method
            HandEvaluator.SortCardsDescending(_cards);
            if (IsCombinationPresent()) return;

            //Modify this method to work with Set and FourOfAKind
            List<Card> pair = PopDuplicates(_cards, 2);
            InsertAtFront(pair); 
        } 
    }
}