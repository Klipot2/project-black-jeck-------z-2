using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker
{
    class DealCards : DeckCards
    {
        private Card[] playerHand;
        private Card[] computerHand;
        private Card[] sortedPlayerHand;
        private Card[] sortedComputerHand;
        private int betAmount;
        private int playerBank;

        public DealCards()
        {
            playerHand = new Card[5];
            sortedPlayerHand = new Card[5];
            computerHand = new Card[5];
            sortedComputerHand = new Card[5];
            playerBank = 1000; // Начальный банк игрока
            betAmount = 0;
        }

        public void Deal()
        {
            setUpDeck();
            getHand();
            sortCards();
            displayCards();
            evaluateHands();
        }

        public void getHand()
        {
            for (int i = 0; i < 5; i++)
                playerHand[i] = getDeck[i];

            for (int i = 5; i < 10; i++)
                computerHand[i - 5] = getDeck[i];
        }

        public void sortCards()
        {
            if (playerHand != null && computerHand != null)
            {
                var queryPlayer = from hand in playerHand
                                  orderby hand.CardValue
                                  select hand;

                var queryComputer = from hand in computerHand
                                    orderby hand.CardValue
                                    select hand;

                var index = 0;
                foreach (var element in queryPlayer.ToList())
                {
                    sortedPlayerHand[index] = element;
                    index++;
                }
                index = 0;
                foreach (var element in queryComputer.ToList())
                {
                    sortedComputerHand[index] = element;
                    index++;
                }
            }
        }

        public void displayCards()
        {
            Console.Clear();
            int x = 0;
            int y = 1;

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("PLAYER Hand");
            for (int i = 0; i < 5; i++)
            {
                DrawCards.DrawCardOutline(x, y);
                DrawCards.DrawCardSuitValue(sortedPlayerHand[i], x, y);
                x++;
            }
            y = 15;
            x = 0;
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Computer Hand");
            for (int i = 5; i < 10; i++)
            {
                DrawCards.DrawCardOutline(x, y);
                DrawCards.DrawCardSuitValue(sortedComputerHand[i - 5], x, y);
                x++;
            }
        }

        public void evaluateHands()
        {
            if (playerBank <= 0)
            {
                Console.WriteLine("Player is out of money. Game over!");
                return;
            }

            HandEvaluator playerHandEvaluator = new HandEvaluator(sortedPlayerHand, betAmount);
            HandEvaluator computerHandEvaluator = new HandEvaluator(sortedComputerHand, betAmount);

            Hand playerHand = playerHandEvaluator.EvaluateHand();
            Hand computerHand = computerHandEvaluator.EvaluateHand();

            Console.WriteLine("\n\n\n\n\nPlayer Hand: " + playerHand);
            Console.WriteLine("\nComputer Hand: " + computerHand);

            if (playerHand < computerHand)
            {
                Console.WriteLine("Player Wins!");
                playerBank += GetBetAmount(); // Прибавляем ставку к банку игрока
                SetBetAmount(0);
            }
            else if (playerHand > computerHand)
            {
                Console.WriteLine("Computer WINS!");
                playerBank -= GetBetAmount(); // Вычитаем ставку из банка игрока
                SetBetAmount(0);
            }
            else
            {
                if (playerHandEvaluator.HandValues.Total > computerHandEvaluator.HandValues.Total)
                {
                    Console.WriteLine("Player WINS");
                    playerBank += GetBetAmount();
                    SetBetAmount(0);
                }
                else if (playerHandEvaluator.HandValues.Total < computerHandEvaluator.HandValues.Total)
                {
                    Console.WriteLine("Computer WINS");
                    playerBank -= GetBetAmount();
                    SetBetAmount(0);
                }
                else
                {
                    if (playerHandEvaluator.HandValues.HighCard > computerHandEvaluator.HandValues.HighCard)
                    {
                        Console.WriteLine("Player WINS");
                        playerBank += GetBetAmount();
                        SetBetAmount(0);
                    }
                    else if (playerHandEvaluator.HandValues.HighCard < computerHandEvaluator.HandValues.HighCard)
                    {
                        Console.WriteLine("Computer WINS");
                        playerBank -= GetBetAmount();
                        SetBetAmount(0);
                    }
                    else
                    {
                        Console.WriteLine("Draw, no one WINS");
                    }
                }
            }

            Console.WriteLine("Your current bank: " + playerBank);
        }

        public void SetBetAmount(int amount)
        {
            betAmount = amount;
        }

        public int GetBetAmount()
        {
            return betAmount;
        }
    }
}