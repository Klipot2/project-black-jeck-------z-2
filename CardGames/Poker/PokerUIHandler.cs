namespace Casino.CardGames.Poker
{
    public static class PokerUIHandler
    {
        public delegate void ProcessInput(Input input);
        public delegate void ProcessInt(int inputInt);

        public static void DisplayPlayerStatus(PokerHand hand)
        {
            DisplayHand(hand);
            Console.WriteLine("Current chip count: " + hand.Bank);
            Console.WriteLine("Current bet: " + hand.Bet);
            Console.WriteLine();
        }
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

        public static void NoResponseMessage(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine();
        }

        private static string MessageWithResponse(string message)
        {
            Console.WriteLine(message);
            string? input = Console.ReadLine() ?? throw new ArgumentNullException("Input ended up as null.");
            Console.WriteLine();
            return input.ToLower();
        }

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
                Console.Write("Input is not valid in current context. ");
                MessageWithInputResponse(fallbackMessage, possibleInputs, fallbackMessage, inputProcessor);
                return;
            }

            inputProcessor(parsedInput);
        }

        public static void MessageWithNumericResponse(string message, List<int> possibleIntInputs,
            string fallbackMessage, ProcessInt intProcessor)
        {
            string response = MessageWithResponse(message);

            if(!int.TryParse(response, out int parsedInt))
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

        private static string ReplaceCharacterAt(string input, int index, char newChar)
        {
            if (input == null)
                throw new ArgumentNullException("Trying to replace character, but string is null.");

            if (index >= input.Length)
                throw new ArgumentOutOfRangeException(
                string.Format("Trying to replace character on position {0}, but string is only {1} characters long.",
                index, input.Length));

            char[] chars = input.ToCharArray();
            chars[index] = newChar;
            return new string(chars);
        }
    }
}
