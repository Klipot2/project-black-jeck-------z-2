namespace Casino.CardGames
{
    /// <summary>
    /// Represents a deck of playing cards.
    /// </summary>
    public class DeckCards
    {
        /// <summary>
        /// Gets the current size of the deck.
        /// </summary>
        public int DeckSize { get { return _deck.Count; } }

        private List<Card> _deck; // List of cards in the deck
        private readonly Random _rand = new Random(); // Random number generator

        /// <summary>
        /// Initializes a new instance of the DeckCards class.
        /// </summary>
        public DeckCards()
        {
            _deck = new List<Card>();
        }

        /// <summary>
        /// Adds a card to the deck.
        /// </summary>
        /// <param name="card">The card to be added.</param>
        public void AddCard(Card card)
        {
            _deck.Add(card);
        }

        /// <summary>
        /// Draws a card from the deck.
        /// </summary>
        /// <param name="positionFromTop">The position of the card from the top of the deck.</param>
        /// <returns>The drawn card.</returns>
        public Card DrawCard(int positionFromTop = 0)
        {
            // Checks for the validity of the index
            if (positionFromTop < 0 || positionFromTop >= DeckSize)
            {
                throw new ArgumentOutOfRangeException(nameof(positionFromTop), "Invalid card index.");
            }

            Card cardToDraw = _deck[positionFromTop];
            _deck.RemoveAt(positionFromTop);
            return cardToDraw;
        }

        /// <summary>
        /// Draws a card from the bottom of the deck.
        /// </summary>
        /// <param name="positionFromBottom">The position of the card from the bottom of the deck.</param>
        /// <returns>The drawn card.</returns>
        public Card DrawFromBottom(int positionFromBottom = 0) =>
            DrawCard(DeckSize - 1 - positionFromBottom);

        /// <summary>
        /// Draws a specified number of cards from the deck.
        /// </summary>
        /// <param name="amountOfCards">The number of cards to draw.</param>
        /// <returns>The drawn cards.</returns>
        public List<Card> DrawCards(int amountOfCards)
        {
            // Checks for the validity of the number of cards
            if (amountOfCards <= 0 || amountOfCards > DeckSize)
            {
                throw new ArgumentOutOfRangeException(nameof(amountOfCards), "Invalid number of cards to draw.");
            }

            List<Card> drawnCards = new List<Card>();
            for (int i = 0; i < amountOfCards; i++)
            {
                Card card = DrawCard();
                drawnCards.Add(card);
            }
            return drawnCards;
        }

        /// <summary>
        /// Draws a specific card from the deck.
        /// </summary>
        /// <param name="card">The card to draw.</param>
        /// <returns>The drawn card.</returns>
        public Card DrawExactCard(Card card)
        {
            // Checks for the presence of the card in the deck
            if (!_deck.Contains(card))
            {
                throw new ArgumentException("The specified card is not in the deck.", nameof(card));
            }

            _deck.Remove(card);
            return card;
        }

        /// <summary>
        /// Draws a random card from the deck.
        /// </summary>
        /// <returns>The drawn card.</returns>
        public Card DrawRandomCard()
        {
            int cardToDrawIndex = _rand.Next(DeckSize);
            return DrawCard(cardToDrawIndex);
        }

        /// <summary>
        /// Sets up the deck with the specified number of full decks and shuffles the cards.
        /// </summary>
        /// <param name="amountOfFullDecks">The number of full decks to include in the deck.</param>
        public void SetUpDeck(int amountOfFullDecks = 1)
        {
            // Checks for the validity of the number of decks
            if (amountOfFullDecks <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amountOfFullDecks), "Invalid number of decks.");
            }

            ResetDeck();

            // Creating and adding cards to the deck
            for (int i = 0; i < amountOfFullDecks; i++)
            {
                foreach (Card.Suit suit in Enum.GetValues(typeof(Card.Suit)))
                {
                    foreach (Card.Value value in Enum.GetValues(typeof(Card.Value)))
                    {
                        Card card = new Card(suit, value);
                        AddCard(card);
                    }
                }
            }

            ShuffleCards();
        }

        /// <summary>
        /// Shuffles the cards in the deck.
        /// </summary>
        public void ShuffleCards()
        {
            List<Card> shuffledDeck = new List<Card>();
            while (DeckSize > 0)
            {
                Card randomCard = DrawRandomCard();
                shuffledDeck.Add(randomCard);
            }

            _deck = shuffledDeck;
        }

        /// <summary>
        /// Resets the deck by creating a new empty deck.
        /// </summary>
        private void ResetDeck()
        {
            _deck = new List<Card>();
        }

        /// <summary>
        /// Gets a list of all cards in the deck.
        /// </summary>
        /// <returns>The list of cards in the deck.</returns>
        public List<Card> GetAllCards() => _deck;
    }
}
