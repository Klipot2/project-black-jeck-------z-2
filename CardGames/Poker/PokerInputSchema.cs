namespace Casino.CardGames.Poker
{
    public enum LetterInput
    {
        Undefined, Yes, No
    }

    public static class PokerInputSchema
    {
        public static Dictionary<LetterInput, string[]> GetInputDictionary()
        {
            Dictionary<LetterInput, string[]> inputDictionary = [];
            string[] yesStrings = ["y"];
            inputDictionary.Add(LetterInput.Yes, yesStrings);
            string[] noStrings = ["n"];
            inputDictionary.Add(LetterInput.No, noStrings);

            return inputDictionary;
        }

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