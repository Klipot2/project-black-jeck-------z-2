namespace Casino.CardGames.Poker
{
    public static class WeightedRNG
    {
        public static int[] GenerateWeightedDistribution(int middleIndex, int distributionSize)
        {
            const int MIN_WEIGHT = 1;
            const int MIN_WEIGHT_STEP = 2;
            const int MAX_WEIGHT_STEP = 5;

            int[] weightedDistribution = new int[distributionSize];
            bool isRightLeaning = middleIndex >= distributionSize / 2;
            int weight = MIN_WEIGHT;
            int maxAdjustedWeight = (int)Math.Round((1 - Math.Abs(1 - 2 * (float)middleIndex/distributionSize))*MAX_WEIGHT_STEP);
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

        public static int ChooseIndexFromWeightedDistribution(int[] weightedDistribution)
        {
            Random rand = new();
            List<int> indexPool = [];
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