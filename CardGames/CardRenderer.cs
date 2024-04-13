namespace Casino.CardGames
{
    /// <summary>
    /// Class responsible for rendering playing cards.
    /// </summary>
    public class CardRenderer
    {
        /// <summary>
        /// Returns a string representing the top part of the card.
        /// </summary>
        private static string GetCardTop() => " ____ ";

        /// <summary>
        /// Returns a string representing the top part of the middle section of the card, considering the value and suit.
        /// </summary>
        private static string GetCardMiddleTop(Card card)
        {
            string cardPositionStatus = GetCardValueSymbol(card.CardValue);
            string suitSymbol = SuitToString(card.CardSuit);

            int spaces = 2 - cardPositionStatus.Length; // Calculate the number of spaces for alignment
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

        /// <summary>
        /// Returns a string representing the bottom part of the middle section of the card.
        /// </summary>
        private static string GetCardMiddleBottom() => "|    |";

        /// <summary>
        /// Returns a string representing the bottom part of the card.
        /// </summary>
        private static string GetCardBottom() => "|____|";

        /// <summary>
        /// Prints five cards from the given collection to the console.
        /// </summary>
        public static void PrintCards(List<Card> cards)
        {
            string outputTop = "";
            string outputMiddleTop = "";
            string outputMiddleBottom = "";
            string outputBottom = "";

            // For each card in the collection, build strings for the top, middle, and bottom parts of the card
            foreach (var card in cards)
            {
                outputTop += GetCardTop() + " ";
                outputMiddleTop += GetCardMiddleTop(card) + " ";
                outputMiddleBottom += GetCardMiddleBottom() + " ";
                outputBottom += GetCardBottom() + " ";
            }

            // Output the strings to the console
            Console.WriteLine(outputTop);
            Console.WriteLine(outputMiddleTop);
            Console.WriteLine(outputMiddleBottom);
            Console.WriteLine(outputBottom);
        }

        /// <summary>
        /// Returns the suit symbol for the given card suit.
        /// </summary>
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
