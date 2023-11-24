namespace Casino.CardGames
{
    public static class CardVisualizerStub
    {
        public static void VisualizeCard(Card card)
        {
            VisualizeCard("", card);
        }

        public static void VisualizeCard(string prefix, Card card)
        {
            Console.WriteLine(prefix + card.ToString());
        }

        public static void VisualizeHand(Hand hand)
        {
            int i = 0;
            foreach (var card in hand.GetCards())
            {
                i++;
                string prefix = i.ToString() + ". ";
                VisualizeCard(prefix, card);
            }
        }
    }
}