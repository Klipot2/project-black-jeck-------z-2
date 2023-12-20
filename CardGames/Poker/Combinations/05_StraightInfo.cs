namespace Casino.CardGames.Poker.Combinations
{
    public class StraightInfo : CombinationInfo
    {
        public StraightInfo(List<Card> cards) : base(cards) { }

        protected override bool IsCombinationPresent() => IsStraight(_valueComposition);

        protected override void SortCards()
        {
            base.SortCards();

            if (_valueComposition[Card.Value.Ace] * _valueComposition[Card.Value.Two] != 0)
            {
                // May be a problem with ace being a reference.
                Card ace = _cards[0];
                _cards.RemoveAt(0);
                _cards.Add(ace);
            }
        }

        public static bool IsStraight(Dictionary<Card.Value, int> valueComposition)
        {
            int fromAceToFiveProduct = valueComposition[Card.Value.Ace] 
                * valueComposition[Card.Value.Two] 
                * valueComposition[Card.Value.Three]
                * valueComposition[Card.Value.Four]
                * valueComposition[Card.Value.Five];
            if (fromAceToFiveProduct != 0) return true;

            string valuesAsString = "";
            foreach (var value in valueComposition.Values)
            {
                valuesAsString += value.ToString();
            }

            return valuesAsString.Contains("11111");
        }   
    }
}