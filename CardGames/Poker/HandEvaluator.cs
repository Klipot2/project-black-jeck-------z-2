using Casino.CardGames.Poker.Combinations;

namespace Casino.CardGames.Poker
{
    /// <summary>
    /// Provides methods for evaluating poker hands and sorting cards.
    /// </summary>
    public static class HandEvaluator
    {
        private const int LOWEST_CARD_VALUE = 2;

        /// <summary>
        /// Calculates the value of various combinations in a poker hand.
        /// </summary>
        /// <param name="handCards">The list of cards in the hand.</param>
        /// <param name="valueData">The object to store calculated hand values.</param>
        public static void CalculateHandValueData(List<Card> handCards, ValueData valueData)
        {
            List<CombinationInfo> combinations =
            [
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
            ];

            valueData.ResetData();
            foreach (var combination in combinations)
            {
                valueData.AddValueDataAtFront(combination.CombinationValue);
            }
        }

        /// <summary>
        /// Sorts the cards in the hand in ascending order.
        /// </summary>
        /// <param name="hand">The poker hand to be sorted.</param>
        public static void SortHand(Hand hand)
        {
            List<Card> cards = hand.GetCards();
            SortCardsAscending(cards);
            hand.ResetHand();
            hand.AddCards(cards);
        }

        /// <summary>
        /// Sorts a list of cards in ascending order based on their values.
        /// </summary>
        /// <param name="cards">The list of cards to be sorted.</param>
        public static void SortCardsAscending(List<Card> cards)
        {
            cards.Sort(delegate (Card a, Card b)
            {
                return ValueCard(a) - ValueCard(b);
            });
        }

        /// <summary>
        /// Sorts a list of cards in descending order based on their values.
        /// </summary>
        /// <param name="cards">The list of cards to be sorted.</param>
        public static void SortCardsDescending(List<Card> cards)
        {
            SortCardsAscending(cards);
            cards.Reverse();
        }

        /// <summary>
        /// Assigns a numeric value to a card based on its rank.
        /// </summary>
        /// <param name="card">The card to evaluate.</param>
        /// <returns>The numeric value assigned to the card.</returns>
        public static int ValueCard(Card card)
        {
            string[] namesOfAllCards = Enum.GetNames(typeof(Card.Value));
            string? cardName = Enum.GetName(typeof(Card.Value), card.CardValue);

            int cardValue = Array.IndexOf(namesOfAllCards, cardName) + LOWEST_CARD_VALUE;

            return cardValue;
        }
    }
}
