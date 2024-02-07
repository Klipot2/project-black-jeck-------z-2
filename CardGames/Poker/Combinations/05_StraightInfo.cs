using System.Diagnostics;
using Castle.Core.Internal;

namespace Casino.CardGames.Poker.Combinations
{
    public class StraightInfo : CombinationInfo
    {
        public StraightInfo(List<Card> cards) : base(cards) { }

        protected override bool IsCombinationPresent() => IsStraight(_valueComposition);

        protected override void SortCards()
        {
            base.SortCards();

            List<Card.Value> straightComposition = GetStraightComposition(_valueComposition);
            List<Card> straight = PopCardsFromComposition(_cards, straightComposition);
            InsertAtFront(straight);    
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
                if (value > 0) valuesAsString += "1";
                else valuesAsString += "0";
            }

            return valuesAsString.Contains("11111");
        }
        
        private static List<Card.Value> GetStraightComposition(Dictionary<Card.Value, int> valueComposition)
        {
            List<Card.Value> potentialStraight = [];
            List<Card.Value> confirmedStraight = [];

            if (valueComposition[Card.Value.Ace] > 0)
            {
                potentialStraight.Add(Card.Value.Ace);
            }            

            foreach (var cardValueAndAmountPair in valueComposition)
            {
                if (cardValueAndAmountPair.Value > 0)
                {
                    potentialStraight.Add(cardValueAndAmountPair.Key);
                    if (potentialStraight.Count > 5)
                    {
                        potentialStraight.RemoveAt(0);
                    }
                    if (potentialStraight.Count == 5)
                    {
                        confirmedStraight = new(potentialStraight);
                    }
                }
                else
                {
                    potentialStraight.Clear();
                }
            }
            
            if (confirmedStraight.IsNullOrEmpty())
                throw new ArgumentNullException(
                string.Format("{0} does not contain straight, so couldn't find one!",
                valueComposition));
               
            return confirmedStraight;          
        }

        public static List<Card> PopStraightFromCards(List<Card> cards)
        {
            Dictionary<Card.Value, int> valueComposition = GenerateValueComposition(cards);
            List<Card.Value> straightValueComposition = GetStraightComposition(valueComposition);
            List<Card> straightCards = PopCardsFromComposition(cards, straightValueComposition);
            return straightCards;
        }
    }
}