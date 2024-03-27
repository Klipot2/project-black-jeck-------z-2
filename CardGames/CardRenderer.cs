namespace Casino.CardGames
{
    public class CardRenderer
    {
        // Возвращает строку, представляющую верхнюю часть карты
        private static string GetCardTop() => " ____ ";

        // Возвращает строку, представляющую верхнюю часть средней части карты с учетом значения и масти
        private static string GetCardMiddleTop(Card card)
        {
            string cardPositionStatus = GetCardValueSymbol(card.CardValue);
            string suitSymbol = SuitToString(card.CardSuit);

            int spaces = 2 - cardPositionStatus.Length; // Вычисляем количество пробелов для выравнивания
            return $"|{new string(' ', spaces)}{cardPositionStatus}{suitSymbol} |";
        }

        private static string GetCardValueSymbol(Card.Value value)
        {
            return value switch
            {
                Card.Value.Two => "2",
                Card.Value.Three => "3",
                Card.Value.Four => "4",
                Card.Value.Five => "5",
                Card.Value.Six => "6",
                Card.Value.Seven => "7",
                Card.Value.Eight => "8",
                Card.Value.Nine => "9",
                Card.Value.Ten => "10",
                Card.Value.Jack => "J",
                Card.Value.Queen => "Q",
                Card.Value.King => "K",
                Card.Value.Ace => "A",
                _ => throw new ArgumentException("Couldn't recognize card value to convert it into string."),
            };
        }

        // Возвращает строку, представляющую нижнюю часть средней части карты
        private static string GetCardMiddleBottom() => "|    |";

        // Возвращает строку, представляющую нижнюю часть карты
        private static string GetCardBottom() => "|____|";

        // Выводит на консоль пять карт из переданной коллекции
        public static void PrintCards(List<Card> cards)
        {
            string outputTop = "";
            string outputMiddleTop = "";
            string outputMiddleBottom = "";
            string outputBottom = "";

            // Для каждой карты из коллекции строим строки для верхней, средней и нижней части карты
            foreach (var card in cards)
            {
                outputTop += GetCardTop() + " ";
                outputMiddleTop += GetCardMiddleTop(card) + " ";
                outputMiddleBottom += GetCardMiddleBottom() + " ";
                outputBottom += GetCardBottom() + " ";
            }

            // Выводим строки на консоль
            Console.WriteLine(outputTop);
            Console.WriteLine(outputMiddleTop);
            Console.WriteLine(outputMiddleBottom);
            Console.WriteLine(outputBottom);
        }

        // Возвращает символ масти для заданной масти карты
        private static string SuitToString(Card.Suit suit)
        {
            return suit switch
            {
                Card.Suit.H => "\u2665", // ♥
                Card.Suit.S => "\u2660", // ♠
                Card.Suit.C => "\u2663", // ♣
                Card.Suit.D => "\u2666", // ♦
                _ => throw new ArgumentException("Couldn't recognize card suit to convert it into string."),
            };
        }
    }
}
