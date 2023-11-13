namespace Casino.CardGames
{
    public class DebugPoker : IPlayable
    {
        public void Play()
        {
            DeckCards deck = new();
            deck.SetUpDeck();
            Hand playerHand = new();
            Card card = deck.DrawCard();
            playerHand.AddCard(card);
            DrawCards.DrawCardOutline(0, 0);
            DrawCards.DrawCardSuitValue(card, 0, 0);
            Console.WriteLine("Game under construction");
        }
    }
}