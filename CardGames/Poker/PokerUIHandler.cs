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
        public delegate void ProcessInput(LetterInput input);

        /// <summary>
        /// Delegate for processing user input of type int.
        /// </summary>
        /// <param name="inputInt">The user input of type int.</param>
        public delegate void ProcessInt(int inputInt);

        public static void DisplayDivider(int dividerLength = 50)
        {
            for (int i = 0; i < dividerLength; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }
        public static void DisplayPlayerStatus(PokerHand hand)
        {
            DisplayHand(hand);
            Console.WriteLine("Current chip count: " + hand.Bank);
            Console.WriteLine("Current bet: " + hand.Bet);
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
        public static void NoResponseMessage(string message, bool spaceAfterMessage = true)
        {
            Console.WriteLine(message);
            if (spaceAfterMessage) Console.WriteLine();
        }

        private static string MessageWithResponse(string message)
        {
            Console.WriteLine(message);
            string? input = Console.ReadLine() ?? throw new ArgumentNullException("Input ended up as null.");
            Console.WriteLine();
            return input.ToLower();
        }

        public static void MessageWithInputResponse(string message, LetterInput[] possibleInputs,
            string fallbackMessage, ProcessInput inputProcessor)
        {
            string response = MessageWithResponse(message);

            if (!PokerInputSchema.TryGetInputFromString(response, out LetterInput parsedInput))
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

        public static void MessageWithRangeResponse(string message, int minRangeValue, int maxRangeValue,
            string fallbackMessage, ProcessInt intProcessor)
        {
            string response = MessageWithResponse(message);

            if(!int.TryParse(response, out int parsedInt))
            {
                Console.Write("Current input cannot contain non-numeric values. ");
                MessageWithRangeResponse(fallbackMessage, minRangeValue, maxRangeValue, fallbackMessage, intProcessor);
                return;
            }

            if (parsedInt < minRangeValue || parsedInt > maxRangeValue)
            {
                Console.Write("Current input is out of range. ");
                MessageWithRangeResponse(fallbackMessage, minRangeValue, maxRangeValue, fallbackMessage, intProcessor);
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
            ArgumentNullException.ThrowIfNull(input, "Trying to replace character, but string is null.");

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
