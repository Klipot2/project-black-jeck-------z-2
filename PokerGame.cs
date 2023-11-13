// using System;

// namespace Poker
// {
//     class PokerGame
//     {
//         public void PlayGame()
//         {
//             DealCards dc = new DealCards();
//             Console.WriteLine();
//             Console.WriteLine();
//             Console.WriteLine();
//             Console.WriteLine();
//             Console.WriteLine();
//             Console.WriteLine();
//             Console.WriteLine();
//             bool quit = false;

//             while (!quit)
//             {
//                 Console.WriteLine("Enter your bet amount:");
//                 string betInput = Console.ReadLine();

//                 if (int.TryParse(betInput, out int bet))
//                 {
//                     dc.SetBetAmount(bet);
//                 }
//                 else
//                 {
//                     Console.WriteLine("Invalid bet amount. Please enter a valid number.");
//                 }

//                 dc.Deal();

//                 char selection = ' ';
//                 while (!selection.Equals('Y') && !selection.Equals('N'))
//                 {
//                     Console.WriteLine("Play again? Y-N");

//                     string inputString = Console.ReadLine();

//                     if (IsInputValid(inputString, out char inputChar))
//                     {
//                         selection = inputChar;
//                     }
//                 }

//                 if (selection.Equals('Y'))
//                     quit = false;
//                 else if (selection.Equals('N'))
//                     quit = true;
//                 else
//                     Console.WriteLine("Invalid Selection. Try again");

//                 Console.WriteLine("Your current bet amount: " + dc.GetBetAmount());
//             }
//             Console.ReadKey();
//         }

//         private bool IsInputValid(string input, out char inputChar)
//         {
//             inputChar = ' ';
//             if (!string.IsNullOrEmpty(input) && input.Length == 1)
//             {
//                 inputChar = input[0];
//                 return inputChar == 'Y' || inputChar == 'N';
//             }
//             return false;
//         }
//     }
// }