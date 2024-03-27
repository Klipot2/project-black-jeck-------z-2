namespace Casino.CardGames.Poker.Combinations
{
    public class FourOfAKindInfo : CombinationInfo
    {
        public FourOfAKindInfo(List<Card> cards) : base(cards) { }

        protected override bool IsCombinationPresent() => CompositionContainsOneOf([4]);

        protected override void SortCards()
        {
            base.SortCards();

            List<Card> fourOfAKind = PopDuplicates(_cards);
            InsertAtFront(fourOfAKind); 
        }           
    }
}