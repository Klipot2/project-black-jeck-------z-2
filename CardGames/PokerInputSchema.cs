namespace Casino.CardGames
{
    public enum Input
    {
        Undefined, Yes, No
    }

    public static class PokerInputSchema
    {
        public static Dictionary<Input, string[]> GetInputDictionary()
        {
            Dictionary<Input, string[]> inputDictionary = new();
            string[] yesStrings = {"y"};
            inputDictionary.Add(Input.Yes, yesStrings);
            string[] noStrings = {"n"};
            inputDictionary.Add(Input.No, noStrings);

            return inputDictionary;
        }

        public static bool TryGetInputFromString(string potentialInput, out Input input)
        {
            input = Input.Undefined;
            Dictionary<Input, string[]> inputDict = GetInputDictionary();

            foreach (KeyValuePair<Input, string[]> inputPair in inputDict)
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