using System;
using System.IO;
 
namespace Poker
{
    class Program
    {
        static Game game;
        static StreamWriter file;
 
        static void Main(string[] args)
        {
            game = new Game();
 
            //for (int j = 0; j < 10000; j++)
            //{
 
            // Don't make it more than 10 players 
            int numberOfPlayers = 2;
 
            for (int i = 0; i < numberOfPlayers; i++)
            {
                var cards = game.DealCards();
                var summary = game.GetHandSummary(cards);
 
                Write(summary.Item2, true);
 
                foreach (Card card in cards)
                {
                    string suit = GetSuitSign(card.Suit);
                    string content = card.Rank + suit + ", ";
                    Write(content, false);
                }
 
                Write(string.Empty, true);
            }
 
            //game.Reset();
 
            //Write(string.Empty, true);
            //}
 
            file.Close();
            Console.ReadLine();
        }
 
        static string GetSuitSign(char suit)
        {
            string suitSign = string.Empty;
 
            switch (suit.ToString())
            {
                case "S":
                    suitSign = "♠";
                    break;
                case "H":
                    suitSign = "♥";
                    break;
                case "C":
                    suitSign = "♣";
                    break;
                case "D":
                    suitSign = "♦";
                    break;
                default:
                    throw new NotImplementedException();
            }
 
            return suitSign;
        }
 
        static StreamWriter CreateFile()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            return new System.IO.StreamWriter(path + @"\test.txt");
        }
 
        static void Write(string text, bool moveToNewLine)
        {
            file = file ?? CreateFile();
 
            if (moveToNewLine)
            {
                Console.WriteLine(text);
                file.WriteLine(text);
            }
            else
            {
                Console.Write(text);
                file.Write(text);
            }
        }
    }
}