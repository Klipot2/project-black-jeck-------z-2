using Casino.CardGames.Poker.Combinations;

namespace Casino.CardGames.Poker
{
    public static class HandEvaluator
    {
        public const int COMBINATION_AMOUNT = 10;
        private const int LOWEST_CARD_VALUE = 2;

        public static void CalculateHandValueData(List<Card> handCards, ValueData valueData)
        {
            List<CombinationInfo> combinations = new()
            {
                new FreeHandInfo(handCards),
                new PairInfo(handCards),
                new DoublePairInfo(handCards),
                new SetInfo(handCards),
                new StraightInfo(handCards),
                new FlushInfo(handCards),
                new FullHouseInfo(handCards),
                new FourOfAKindInfo(handCards),
                new StraightFlushInfo(handCards),
                new FlushRoyaleInfo(handCards)
            };

            valueData.ResetData();
            foreach (var combination in combinations)
            {
                valueData.AddValueDataAtFront(combination.CombinationValue);
            }
        }

        public static void SortHand(Hand hand)
        {
            List<Card> cards = hand.GetCards();
            SortCardsAscending(cards);
            hand.ResetHand();
            hand.AddCards(cards);
        }

        public static void SortCardsAscending(List<Card> cards)
        {
            cards.Sort(delegate(Card a, Card b)
            {
                return ValueCard(a) - ValueCard(b);
            });
        }

        public static void SortCardsDescending(List<Card> cards)
        {
            SortCardsAscending(cards);
            cards.Reverse();
        }

        public static int ValueCard(Card card)
        {
            string[] namesOfAllCards = Enum.GetNames(typeof(Card.Value));
            string? cardName = Enum.GetName(typeof(Card.Value), card.CardValue);

            int cardValue = Array.IndexOf(namesOfAllCards, cardName) + LOWEST_CARD_VALUE;

            return cardValue;
        }  
    }
}