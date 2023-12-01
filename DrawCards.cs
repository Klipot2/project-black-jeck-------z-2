using System;

namespace Casino.CardGames.Poker
{
    public static class DrawCards
    {
        public static void DrawCard(Card card)
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(" ________");
            for (int i = 0; i < 10; i++)
            {
                if (i == 9)
                {
                    Console.WriteLine("|________|");
                }
                else
                {
                    Console.WriteLine("|        |");
                }
            }

            DrawCardSuitValue(card);
        }

        private static void DrawCardSuitValue(Card card)
        {
            char cardSuit = ' ';
            switch (card.CardSuit)
            {
                case Card.Suit.H:
                    cardSuit = '\u2665';
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Card.Suit.D:
                    cardSuit = '\u2666';
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Card.Suit.C:
                    cardSuit = '\u2663';
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case Card.Suit.S:
                    cardSuit = '\u2660';
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }

            Console.SetCursorPosition(6, 5);
            Console.Write(cardSuit);
            Console.SetCursorPosition(5, 7);
            Console.Write(card.CardValue);
        }
    }
}
