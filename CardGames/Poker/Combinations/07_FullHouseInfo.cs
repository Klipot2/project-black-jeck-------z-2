namespace Casino.CardGames.Poker.Combinations
{
    public class FullHouseInfo : CombinationInfo
    {
        public FullHouseInfo(List<Card> cards) : base(cards){}

        protected override bool IsCombinationPresent()
        {
            bool hasPair = false;
            bool hasSet = false;
            foreach (var value in _valueComposition.Values)
            {
                if (value == 2) hasPair = true;
                if (value == 3) hasSet = true;
            }

            return hasSet && hasPair;
        }

        protected override void SortCards()
        {
            base.SortCards();

            List<Card> set = PopDuplicates(_cards);
            List<Card> pair = PopDuplicates(_cards);
            InsertAtFront(pair); 
            InsertAtFront(set); 
        }         
    }
}