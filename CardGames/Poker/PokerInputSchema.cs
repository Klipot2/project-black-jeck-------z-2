namespace Casino.CardGames.Poker
{
    /// <summary>
    /// Represents the possible inputs in the form of letters.
    /// </summary>
    public enum LetterInput
    {
        /// <summary>
        /// Indicates undefined input.
        /// </summary>
        Undefined, 
        /// <summary>
        /// Indicates "Yes" input.
        /// </summary>
        Yes, 
        /// <summary>
        /// Indicates "No" input.
        /// </summary>
        No
    }

    /// <summary>
    /// Provides methods related to poker input handling.
    /// </summary>
    public static class PokerInputSchema
    {
        /// <summary>
        /// Gets a dictionary mapping Input enum values to their corresponding valid string representations.
        /// </summary>
        /// <returns>A dictionary of Input enum values and their string representations.</returns>
        public static Dictionary<LetterInput, string[]> GetInputDictionary()
        {
            Dictionary<LetterInput, string[]> inputDictionary = [];
            string[] yesStrings = ["y"];
            inputDictionary.Add(LetterInput.Yes, yesStrings);
            string[] noStrings = ["n"];
            inputDictionary.Add(LetterInput.No, noStrings);

            return inputDictionary;
        }

        /// <summary>
        /// Tries to get an Input enum value from its string representation.
        /// </summary>
        /// <param name="potentialInput">The potential string representation of the input.</param>
        /// <param name="input">The resulting Input enum value, if successful.</param>
        /// <returns>True if the conversion is successful, false otherwise.</returns>
        public static bool TryGetInputFromString(string potentialInput, out LetterInput input)
        {
            input = LetterInput.Undefined;
            Dictionary<LetterInput, string[]> inputDict = GetInputDictionary();

            foreach (KeyValuePair<LetterInput, string[]> inputPair in inputDict)
            {
                if (inputPair.Value.Contains(potentialInput))
                {
                    input = inputPair.Key;
                    return true;
                }
            }

            return false;
        }
    }
}
