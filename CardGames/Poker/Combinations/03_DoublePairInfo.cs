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
                if (value == 2) pairAmount += 1;
            }

            return pairAmount > 1;
        }

        protected override void SortCards()
        {
            HandEvaluator.SortCardsDescending(_cards);

            if (!IsCombinationPresent()) return;

            List<Card> firstPair = PopDuplicates(_cards, 2);
            List<Card> secondPair = PopDuplicates(_cards, 2);
            InsertAtFront(secondPair); 
            InsertAtFront(firstPair); 
        } 
    }
}