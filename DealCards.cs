// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Casino.CardGames;

// namespace Poker
// {
//     class DealCards
//     {
//         private const int PLAYER_HAND_SIZE = 5;

//         private DeckCards _deck;
//         private Hand _playerHand;
//         private Hand _computerHand;
//         // private Card[] sortedPlayerHand;
//         // private Card[] sortedComputerHand;
//         private int betAmount;
//         private int playerBank;

//         public DealCards()
//         {
//             _deck = new DeckCards();
//             _playerHand = new Hand();
//             _computerHand = new Hand();
//             // sortedPlayerHand = new Card[5];
//             // sortedComputerHand = new Card[5];
//             playerBank = 1000; // Начальный банк игрока
//             betAmount = 0;
//         }

//         public void Deal()
//         {
//             _deck.SetUpDeck();
//             FillHand(_playerHand);
//             FillHand(_computerHand);
//             //sortCards(); // Move to Evaluator
//             //displayCards();
//             evaluateHands();
//         }

//         private void FillHand(Hand hand)
//         {
//             List<Card> fullHand = _deck.DrawCards(PLAYER_HAND_SIZE);
//             hand.AddCards(fullHand);
//         }

//         public void sortCards()
//         {
//             if (playerHand != null && computerHand != null)
//             {
//                 var queryPlayer = from hand in playerHand
//                                   orderby hand.CardValue
//                                   select hand;

//                 var queryComputer = from hand in computerHand
//                                     orderby hand.CardValue
//                                     select hand;

//                 var index = 0;
//                 foreach (var element in queryPlayer.ToList())
//                 {
//                     sortedPlayerHand[index] = element;
//                     index++;
//                 }
//                 index = 0;
//                 foreach (var element in queryComputer.ToList())
//                 {
//                     sortedComputerHand[index] = element;
//                     index++;
//                 }
//             }
//         }

//         // public void displayCards()
//         // {
//         //     Console.Clear();
//         //     int x = 0;
//         //     int y = 1;

//         //     Console.ForegroundColor = ConsoleColor.DarkCyan;
//         //     Console.WriteLine("PLAYER Hand");
//         //     for (int i = 0; i < 5; i++)
//         //     {
//         //         DrawCards.DrawCardOutline(x, y);
//         //         DrawCards.DrawCardSuitValue(sortedPlayerHand[i], x, y);
//         //         x++;
//         //     }
//         //     y = 15;
//         //     x = 0;
//         //     Console.SetCursorPosition(x, y);
//         //     Console.ForegroundColor = ConsoleColor.DarkRed;
//         //     Console.WriteLine("Computer Hand");
//         //     for (int i = 5; i < 10; i++)
//         //     {
//         //         DrawCards.DrawCardOutline(x, y);
//         //         DrawCards.DrawCardSuitValue(sortedComputerHand[i - 5], x, y);
//         //         x++;
//         //     }
//         // }

//         public void evaluateHands()
//         {
//             if (playerBank <= 0)
//             {
//                 Console.WriteLine("Player is out of money. Game over!");
//                 return;
//             }

//             HandEvaluator playerHandEvaluator = new HandEvaluator(sortedPlayerHand, betAmount);
//             HandEvaluator computerHandEvaluator = new HandEvaluator(sortedComputerHand, betAmount);

//             Hand playerHand = playerHandEvaluator.EvaluateHand();
//             Hand computerHand = computerHandEvaluator.EvaluateHand();

//             Console.WriteLine("\n\n\n\n\nPlayer Hand: " + playerHand);
//             Console.WriteLine("\nComputer Hand: " + computerHand);

//             if (playerHand < computerHand)
//             {
//                 Console.WriteLine("Player Wins!");
//                 playerBank += GetBetAmount(); // Прибавляем ставку к банку игрока
//                 SetBetAmount(0);
//             }
//             else if (playerHand > computerHand)
//             {
//                 Console.WriteLine("Computer WINS!");
//                 playerBank -= GetBetAmount(); // Вычитаем ставку из банка игрока
//                 SetBetAmount(0);
//             }
//             else
//             {
//                 if (playerHandEvaluator.HandValues.Total > computerHandEvaluator.HandValues.Total)
//                 {
//                     Console.WriteLine("Player WINS");
//                     playerBank += GetBetAmount();
//                     SetBetAmount(0);
//                 }
//                 else if (playerHandEvaluator.HandValues.Total < computerHandEvaluator.HandValues.Total)
//                 {
//                     Console.WriteLine("Computer WINS");
//                     playerBank -= GetBetAmount();
//                     SetBetAmount(0);
//                 }
//                 else
//                 {
//                     if (playerHandEvaluator.HandValues.HighCard > computerHandEvaluator.HandValues.HighCard)
//                     {
//                         Console.WriteLine("Player WINS");
//                         playerBank += GetBetAmount();
//                         SetBetAmount(0);
//                     }
//                     else if (playerHandEvaluator.HandValues.HighCard < computerHandEvaluator.HandValues.HighCard)
//                     {
//                         Console.WriteLine("Computer WINS");
//                         playerBank -= GetBetAmount();
//                         SetBetAmount(0);
//                     }
//                     else
//                     {
//                         Console.WriteLine("Draw, no one WINS");
//                     }
//                 }
//             }

//             Console.WriteLine("Your current bank: " + playerBank);
//         }

//         public void SetBetAmount(int amount)
//         {
//             betAmount = amount;
//         }

//         public int GetBetAmount()
//         {
//             return betAmount;
//         }
//     }
// }