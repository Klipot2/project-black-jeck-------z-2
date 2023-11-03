using System;

namespace Poker
{
    class Program
    {
        static void Main(string[] args)
        {
            if (OperatingSystem.IsWindows())
            {
                Console.SetWindowSize(65, 40);
                Console.BufferWidth = 65;
                Console.BufferHeight = 40;
            }
            Console.Title = "Game Selection";
            Console.WriteLine();
            Console.WriteLine("Select a game to play:");
            Console.WriteLine("1. Poker Game");
            Console.WriteLine("2. Triangle Drawer Game");
            Console.WriteLine("3. Dice Game");
            Console.WriteLine("4. Guessing Game");
            Console.WriteLine("Enter the number of the game to play:");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    PlayPokerGame();
                    break;
                case "2":
                    PlayTriangleDrawerGame();
                    break;
                case "3":
                    PlayDiceGame();
                    break;
                case "4":
                    PlayGuessingGame();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Exiting.");
                    break;
            }
        }

        static void PlayPokerGame()
        {
            Console.Title = "Poker Game";
            Console.WriteLine();
            Console.WriteLine();
            PokerGame game = new PokerGame();
            game.PlayGame();
        }
        static void PlayTriangleDrawerGame()
        {
            TriangleDrawer drawer = new TriangleDrawer();
            drawer.ReadHeightFromInput();
            drawer.DrawTriangle();
        }
        static void PlayDiceGame()
        {
            DiceGame game = new DiceGame();
            game.Throw();
        }
        static void PlayGuessingGame()
        {
            GuessingGameRight game = new GuessingGameRight();
            game.StartGame();
        }
    }
}