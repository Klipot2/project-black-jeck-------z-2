namespace Casino.CardGames.Poker
{
    public static class HandEvaluator
    {
        private const int LOWEST_CARD_VALUE = 2;
        private const int HIGHEST_COMBINATION_VALUE = 1000;
        private const int COMBINATION_STEP = 100;

        /*
        1. Старшая карта (14, 13, 12, 11, 9 < 60)
        2. Пара (50*номинал пары) 14*50 = 700 +
        3. Две пары (400*номинал старшей пары) 14*400 = 5600 2
        4. Сет (три одинаковых) (3000*номинал тройки) 14*3000 = 42000 +
        5. Стрит 50000 +(?)
        6. Флеш 60000 - look at 1
        7. Фулл-хаус (4*2) 2*3000*3*50 = 300*3000 = 900000 2
        8. Каре +
        9. Стрит-флеш (5+6) +(?)
        10. Флеш-рояль (9, особое) +
        Словарь кол-ва карт на руке + мастей на руке
        Если какой-то масти 5 - флэш
        Если подряд идет последовательность кол-ва карт 11111 - стрит
        Для любой комбинации запоминаем последовательность старших карт
        */

        public static int GetHandCardsValue(List<Card> handCards)
        {
            Dictionary<Card.Suit, int> suitComposition = new();
            foreach (Card.Suit suit in Enum.GetValues(typeof(Card.Suit)))
            {
                suitComposition[suit] = 0;
            }

            Dictionary<Card.Value, int> valueComposition = new();
            foreach (Card.Value value in Enum.GetValues(typeof(Card.Value)))
            {
                valueComposition[value] = 0;
            }

            foreach (var card in handCards)
            {
                suitComposition[card.CardSuit] += 1;
                valueComposition[card.CardValue] += 1;
            }

            bool hasPair = valueComposition.ContainsValue(2);
            bool hasTwoPairs = HasTwoPairs(valueComposition);
            bool hasThree = valueComposition.ContainsValue(3);
            bool isStraight = HandContainsStraight(valueComposition);
            bool isFlush = suitComposition.ContainsValue(5);
            bool isFullHouse = hasPair && hasThree;
            bool hasFour = valueComposition.ContainsValue(4);
            bool isStraightFlush = isStraight && isFlush;
            bool isFlushRoyale = false;
            if (isStraightFlush) isFlushRoyale = IsFlushRoyale(valueComposition);

            //Compare inside bracket
            bool[] combinations = { isFlushRoyale, isStraightFlush, hasFour, isFullHouse,
                isFlush, isStraight, hasThree, hasTwoPairs, hasPair };

            int handValue = 0;
            int combinationValue = HIGHEST_COMBINATION_VALUE;
            foreach (var combination in combinations)
            {
                if (combination)
                {
                    handValue = combinationValue;
                    break;
                }
                combinationValue -= COMBINATION_STEP;
            }
            // 22345 - 33AKQ : 0202030405 - 0303141312
            // 44225 - 3322A : 0404020205 - 0303020214
            // 44225 - 44226
            return handValue;
        }

        private static bool HasTwoPairs(Dictionary<Card.Value, int> valueComposition)
        {
            int pairAmount = 0;
            foreach (var value in valueComposition.Values)
            {
                if (value == 2) pairAmount += 1;
            }

            return pairAmount > 1;
        }

        private static bool HandContainsStraight(Dictionary<Card.Value, int> valueComposition)
        {
            int fromAceToFiveProduct = valueComposition[Card.Value.Ace] * valueComposition[Card.Value.Two] *
                valueComposition[Card.Value.Three] * valueComposition[Card.Value.Four] * valueComposition[Card.Value.Five];
            if (fromAceToFiveProduct != 0) return true;

            string valuesAsString = "";
            foreach (var value in valueComposition.Values)
            {
                valuesAsString += value.ToString();
            }

            return valuesAsString.Contains("11111");
        }

        private static bool IsFlushRoyale(Dictionary<Card.Value, int> valueComposition)
        {
            int fromTenToAceProduct = valueComposition[Card.Value.Ten] * valueComposition[Card.Value.Jack] *
                valueComposition[Card.Value.Queen] * valueComposition[Card.Value.King] * valueComposition[Card.Value.Ace];
            return fromTenToAceProduct != 0; 
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
            SortCardsDescending(cards);
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