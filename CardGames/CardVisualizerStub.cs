namespace Casino.CardGames
{
    public static class CardVisualizerStub
    {
        public static void VisualizeCard(Card card)
        {
            Console.WriteLine(card.ToString());
        }

        public static void VisualizeHand(Hand hand)
        {
            foreach (var card in hand.GetCards())
            {
                VisualizeCard(card);
            }
        }
    }
}