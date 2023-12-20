namespace Casino.CardGames
{
    /// <summary>
    /// Represents a player's hand in a card game.
    /// </summary>
    public class Hand
    {
        /// <summary>
        /// Gets the number of cards in the hand.
        /// </summary>
        public int Size { get { return _hand.Count; } }

        /// <summary>
        /// Gets the owner's name associated with the hand.
        /// </summary>
        public string Owner { get { return _owner; } }

        private List<Card> _hand; // Список карт в руке

        private string _owner; // Имя владельца руки

        /// <summary>
        /// Initializes a new instance of the Hand class with the specified owner's name.
        /// </summary>
        /// <param name="ownerName">The name of the hand's owner.</param>
        public Hand(string ownerName)
        {
            _hand = new List<Card>();
            _owner = ownerName;
        }

        /// <summary>
        /// Initializes a new instance of the Hand class with the specified owner's name and initial cards.
        /// </summary>
        /// <param name="ownerName">The name of the hand's owner.</param>
        /// <param name="cards">Initial cards in the hand.</param>
        public Hand(string ownerName, List<Card> cards)
        {
            _hand = cards;
            _owner = ownerName;
        }

        /// <summary>
        /// Adds a card to the hand.
        /// </summary>
        /// <param name="card">The card to be added.</param>
        public void AddCard(Card card)
        {
            _hand.Add(card);
        }

        /// <summary>
        /// Adds a list of cards to the hand.
        /// </summary>
        /// <param name="cards">The list of cards to be added.</param>
        public void AddCards(List<Card> cards)
        {
            foreach (var card in cards)
            {
                AddCard(card);
            }
        }

        /// <summary>
        /// Swaps a card in the hand with a new card.
        /// </summary>
        /// <param name="previousCardPosition">The position of the card to be replaced.</param>
        /// <param name="newCard">The new card to be placed in the hand.</param>
        public void SwapCard(int previousCardPosition, Card newCard)
        {
            _hand[previousCardPosition] = newCard;
        }

        /// <summary>
        /// Resets the hand by clearing all cards.
        /// </summary>
        public void ResetHand()
        {
            _hand = new List<Card>();
        }

        /// <summary>
        /// Gets a list of cards in the hand.
        /// </summary>
        /// <returns>The list of cards in the hand.</returns>
        public List<Card> GetCards() => _hand;
    }
}
