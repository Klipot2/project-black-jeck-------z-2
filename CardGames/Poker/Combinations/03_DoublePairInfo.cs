namespace Casino.CardGames.Poker.Combinations
{
    public class DoublePairInfo : CombinationInfo
    {
        public DoublePairInfo(List<Card> cards) : base(cards){}

        protected override bool IsCombinationPresent()
        {
            int pairAmount = 0;
            foreach (var value in _valueComposition.Values)
            {
                if (value >= 2) pairAmount += 1;
            }

            return pairAmount > 1;
        }

        protected override void SortCards()
        {
            base.SortCards();
            
            List<Card> firstPair = PopDuplicates(_cards);
            List<Card> secondPair = PopDuplicates(_cards);
            InsertAtFront(secondPair); 
            InsertAtFront(firstPair); 
        } 
    }
}