using System;
using System.IO;

namespace Poker
{
    class Program
    {
        static CardDeck cardDeck;
        static Hand playerHand;
        static PokerHandEvaluator handEvaluator;
        static StreamWriter file;

        static void Main(string[] args)
        {
            cardDeck = new CardDeck();
            playerHand = new Hand(5); // Например, рука из 5 карт
            handEvaluator = new PokerHandEvaluator();

            game = new Game();

            int numberOfPlayers = 2;

            for (int i = 0; i < numberOfPlayers; i++)
            {
                var cards = game.DealCards();
                var summary = game.GetHandSummary(cards);

                Write(summary.Item2, true);

                foreach (Card card in cards)
                {
                    string suit = GetSuitSign(card.GetSuit());
                    string content = card.GetRank() + suit + ", ";
                    Write(content, false);
                }

                TestPlayerHand();  // Добавляем проверку руки после раздачи

                Write(string.Empty, true);
            }

            file.Close();
            Console.ReadLine();
        }

        static void TestPlayerHand()
        {
            var cards = playerHand.Cards;  // Получаем карты из руки игрока

            // Оцениваем руку игрока
            string handEvaluation = handEvaluator.EvaluateHand(cards);

            // Выводим результат
            Console.WriteLine($"Рука игрока: {handEvaluation}");
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