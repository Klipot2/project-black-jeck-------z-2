using System.Text;

namespace Poker
{
    class DrawCards
    {
        public static void DrawCardOutline(int xcoor, int ycoor)
        {
            Console.ForegroundColor = ConsoleColor.White;

            int x = xcoor * 12;
            int y = ycoor;

            Console.SetCursorPosition(x, y);
            Console.Write(" ________\n");

            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(x, y + 1 + i);

                if (i != 9)
                    Console.WriteLine("|        |");
                else
                    Console.WriteLine("|________|");
            }
        }
        public static void DrawCardSuitValue(Card card, int xcoor, int ycoor)
        {
            char cardSuit = ' ';
            int x = xcoor * 12;
            int y = ycoor;

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
            Console.SetCursorPosition(x + 5, y + 5);
            Console.Write(cardSuit);
            Console.SetCursorPosition(x + 4, y + 7);
            Console.Write(card.CardValue);
        }
    }
}