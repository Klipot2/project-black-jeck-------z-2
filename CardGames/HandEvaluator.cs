using Castle.DynamicProxy.Generators.Emitters.SimpleAST;

namespace Casino.CardGames
{
    public static class HandEvaluator
    {
        private const int LOWEST_CARD_VALUE = 2;

        public static void SortHand(Hand hand)
        {
            List<Card> cards = hand.GetCards();

            cards.Sort(delegate(Card a, Card b)
            {
                return ValueCard(a) - ValueCard(b);
            });

            hand.ResetHand();
            hand.AddCards(cards);
        }

        private static int ValueCard(Card card)
        {
            string[] namesOfAllCards = Enum.GetNames(typeof(Card.Value));
            string? cardName = Enum.GetName(typeof(Card.Value), card.CardValue);

            int cardValue = Array.IndexOf(namesOfAllCards, cardName) + LOWEST_CARD_VALUE;

            return cardValue;
        }
    }
}