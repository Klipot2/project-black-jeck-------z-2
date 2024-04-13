namespace Casino.CardGames.Poker
{
    /// <summary>
    /// Provides methods for generating weighted random numbers.
    /// </summary>
    public static class WeightedRNG
    {
        /// <summary>
        /// Generates a weighted distribution of integers.
        /// </summary>
        /// <param name="middleIndex">The index representing the peak of the distribution.</param>
        /// <param name="distributionSize">The size of the distribution.</param>
        /// <returns>An array representing the weighted distribution.</returns>
        public static int[] GenerateWeightedDistribution(int middleIndex, int distributionSize)
        {
            const int MIN_WEIGHT = 1;
            const int MIN_WEIGHT_STEP = 2;
            const int MAX_WEIGHT_STEP = 5;

            int[] weightedDistribution = new int[distributionSize];
            bool isRightLeaning = middleIndex >= distributionSize / 2;
            int weight = MIN_WEIGHT;
            int maxAdjustedWeight = (int)Math.Round((1 - Math.Abs(1 - 2 * (float)middleIndex / distributionSize)) * MAX_WEIGHT_STEP);
            int adjustedWeightStep = Math.Max(maxAdjustedWeight, MIN_WEIGHT_STEP);
            for (int i = 0; i < distributionSize; i++)
            {
                int index;
                if (isRightLeaning)
                {
                    index = i;
                    weight = index <= middleIndex ? (int)Math.Pow(adjustedWeightStep, i) * MIN_WEIGHT : weight / adjustedWeightStep;
                }
                else
                {
                    index = distributionSize - 1 - i;
                    weight = index >= middleIndex ? (int)Math.Pow(adjustedWeightStep, i) * MIN_WEIGHT : weight / adjustedWeightStep;
                }
                weightedDistribution[index] = weight;
            }
            return weightedDistribution;
        }

        /// <summary>
        /// Chooses an index from a weighted distribution.
        /// </summary>
        /// <param name="weightedDistribution">The weighted distribution.</param>
        /// <returns>The chosen index.</returns>
        public static int ChooseIndexFromWeightedDistribution(int[] weightedDistribution)
        {
            Random rand = new Random();
            List<int> indexPool = new List<int>(); // Corrected list initialization
            for (int i = 0; i < weightedDistribution.Length; i++)
            {
                int indexQuota = weightedDistribution[i];
                while (indexQuota > 0)
                {
                    indexPool.Add(i);
                    indexQuota--;
                }
            }
            int randomPoolIndex = rand.Next(indexPool.Count);
            return indexPool[randomPoolIndex];
        }
    }
}