namespace Casino.CardGames.Poker
{
    public class DebugPoker : IPlayable
    {
        public void Play()
        {
            DeckCards deck = new();
            deck.SetUpDeck();
            Hand playerHand = new("Player");
            Card card = deck.DrawCard();
            playerHand.AddCard(card);
            CardRenderer.PrintFiveCards(playerHand.GetCards()); 
            Console.WriteLine("Game under construction");
        }
    }
}
