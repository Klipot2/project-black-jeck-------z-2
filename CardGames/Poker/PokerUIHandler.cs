```csharp
namespace Casino.CardGames.Poker
{
    /// <summary>
    /// Handles user interface for poker game interactions.
    /// </summary>
    public static class PokerUIHandler
    {
        /// <summary>
        /// Delegate for processing user input of type Input.
        /// </summary>
        /// <param name="input">The user input of type Input.</param>
        public delegate void ProcessInput(Input input);

        /// <summary>
        /// Delegate for processing user input of type int.
        /// </summary>
        /// <param name="inputInt">The user input of type int.</param>
        public delegate void ProcessInt(int inputInt);

        /// <summary>
        /// Displays the player's hand, current chip count, and current bet.
        /// </summary>
        /// <param name="hand">The player's poker hand.</param>
        /// <param name="currentBank">The current chip count of the player.</param>
        /// <param name="currentBet">The current bet in the game.</param>
        public static void DisplayPlayerStatus(PokerHand hand, int currentBank, int currentBet)
        {
            DisplayHand(hand);
            Console.WriteLine("Current chip count: " + currentBank);
            Console.WriteLine("Current bet: " + currentBet);
            Console.WriteLine();
        }

        /// <summary>
        /// Displays a poker hand with an optional debug mode.
        /// </summary>
        /// <param name="hand">The poker hand to display.</param>
        /// <param name="debugMode">Whether to display debug information.</param>
        public static void DisplayHand(PokerHand hand, bool debugMode = false)
        {
            Console.WriteLine(hand.Owner + ":");
            CardRenderer.PrintCards(hand.GetCards());
            if (debugMode)
            {
                Console.WriteLine("----------------------------------");
                Console.WriteLine("Debug info:");
                Console.WriteLine(hand.Value.DebugString());
                Console.WriteLine("----------------------------------");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Displays a message without expecting user input.
        /// </summary>
        /// <param name="message">The message to display.</param>
        public static void NoResponseMessage(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine();
        }

        /// <summary>
        /// Displays a message and prompts the user for an input, processing it based on possible inputs.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="possibleInputs">Array of possible Input enum values.</param>
        /// <param name="fallbackMessage">The fallback message if input validation fails.</param>
        /// <param name="inputProcessor">Delegate to process the valid user input.</param>
        public static void MessageWithInputResponse(string message, Input[] possibleInputs,
            string fallbackMessage, ProcessInput inputProcessor)
        {
            string response = MessageWithResponse(message);

            if (!PokerInputSchema.TryGetInputFromString(response, out Input parsedInput))
            {
                Console.Write("Cannot identify input. ");
                MessageWithInputResponse(fallbackMessage, possibleInputs, fallbackMessage, inputProcessor);
                return;
            }

            if (!possibleInputs.Contains(parsedInput))
            {
                Console.Write("Input is not valid in the current context. ");
                MessageWithInputResponse(fallbackMessage, possibleInputs, fallbackMessage, inputProcessor);
                return;
            }

            inputProcessor(parsedInput);
        }

        /// <summary>
        /// Displays a message and prompts the user for a numeric input, processing it based on possible integers.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="possibleIntInputs">List of possible integer inputs.</param>
        /// <param name="fallbackMessage">The fallback message if input validation fails.</param>
        /// <param name="intProcessor">Delegate to process the valid user input.</param>
        public static void MessageWithNumericResponse(string message, List<int> possibleIntInputs,
            string fallbackMessage, ProcessInt intProcessor)
        {
            string response = MessageWithResponse(message);

            if (!int.TryParse(response, out int parsedInt))
            {
                Console.Write("Current input cannot contain non-numeric values. ");
                MessageWithNumericResponse(fallbackMessage, possibleIntInputs, fallbackMessage, intProcessor);
                return;
            }

            if (!possibleIntInputs.Contains(parsedInt))
            {
                Console.Write("Current input is out of range. ");
                MessageWithNumericResponse(fallbackMessage, possibleIntInputs, fallbackMessage, intProcessor);
                return;
            }

            intProcessor(parsedInt);
        }

        /// <summary>
        /// Converts a list of integers to a formatted string describing card positions for swapping.
        /// </summary>
        /// <param name="swapArray">List of card positions for swapping.</param>
        /// <returns>A formatted string describing card positions for swapping.</returns>
        public static string SwapArrayToString(List<int> swapArray)
        {
            string output = "Currently swapping ";
            output += swapArray.Count > 1 ?
                "cards on positions:" :
                "card on position";
            foreach (var position in swapArray)
            {
                output += " " + position.ToString() + ",";
            }

            return ReplaceCharacterAt(output, output.Length - 1, '.');
        }

        /// <summary>
        /// Replaces a character in a string at a specified index.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="index">The index at which to replace the character.</param>
        /// <param name="newChar">The new character to replace with.</param>
        /// <returns>The modified string.</returns>
        private static string ReplaceCharacterAt(string input, int index, char newChar)
        {
            if (input == null)
                throw new ArgumentNullException("Trying to replace a character, but the string is null.");

            if (index >= input.Length)
                throw new ArgumentOutOfRangeException(
                    string.Format("Trying to replace a character at position {0}, but the string is only {1} characters long.",
                        index, input.Length));

            char[] chars = input.ToCharArray();
            chars[index] = newChar;
            return new string(chars);
        }

        /// <summary>
        /// Displays a message

 and retrieves user input.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <returns>User input as a string.</returns>
        private static string MessageWithResponse(string message)
        {
            Console.WriteLine(message);
            string? input = Console.ReadLine() ?? throw new ArgumentNullException("Input ended up as null.");
            Console.WriteLine();
            return input.ToLower();
        }
    }
}
```
