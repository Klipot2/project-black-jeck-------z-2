namespace Poker
{
    class DeckCards
    {
        public int DeckSize { get{ return _deck.Count; } }

        private List<Card> _deck;
        private readonly Random _rand = new();

        public DeckCards()
        {
            _deck = new List<Card>();
        }

        public void AddCard(Card card)
        {
            _deck.Add(card);
        }

        public Card DrawCard(int positionFromTop = 0)
        {
            if (DeckSize == 0)
                throw new ArgumentOutOfRangeException(
                    "Cannot draw from an empty deck.");
            if (positionFromTop < 0)
                throw new ArgumentOutOfRangeException(
                string.Format("{0} is a negative index, which cannot be accepted by DrawCard.",
                positionFromTop));
            if (positionFromTop >= DeckSize)
                throw new ArgumentOutOfRangeException(
                string.Format("Index {0} is bigger than deck size.",
                positionFromTop));

            Card cardToDraw = _deck[positionFromTop];
            _deck.RemoveAt(positionFromTop);
            return cardToDraw;
        }

        public Card DrawFromBottom(int positionFromBottom = 0) =>
            DrawCard(DeckSize - 1 - positionFromBottom);

        public List<Card> DrawCards(int amountOfCards)
        {
            if (DeckSize < amountOfCards)
                throw new ArgumentOutOfRangeException(
                string.Format("Trying to draw {0}, which is more than current deck size of {1}.",
                amountOfCards, DeckSize));

            List<Card> drawnCards = new();
            for (int i = 0; i < amountOfCards; i++)
            {
                Card card = DrawCard();
                drawnCards.Add(card);
            }
            return drawnCards;
        }

        public Card DrawExactCard(Card card)
        {
            if(!_deck.Contains(card))
                throw new KeyNotFoundException(
                string.Format("Trying to draw {0}, which is not present in the deck.",
                card.ToString()));

            _deck.Remove(card);
            return card;
        }

        public Card DrawRandomCard()
        {
            int cardToDrawIndex = _rand.Next(DeckSize);
            return DrawCard(cardToDrawIndex);
        }

        public void SetUpDeck(int amountOfFullDecks = 1)
        {
            if (amountOfFullDecks <= 0)
                throw new ArgumentOutOfRangeException(
                string.Format("Cannot form a deck from non-positive amount of full decks (currently {0}).",
                amountOfFullDecks));

            for (int i = 0; i < amountOfFullDecks; i++)
            {
                foreach (Card.Suit s in Enum.GetValues(typeof(Card.Suit)))
                {
                    foreach (Card.Value v in Enum.GetValues(typeof(Card.Value)))
                    {
                        Card cardToAdd = new(s, v);
                        AddCard(cardToAdd);
                    }
                }
            }

            ShuffleCards();
        }

        public void ShuffleCards()
        {
            List<Card> shuffledDeck = new();
            while (DeckSize > 0)
            {
                Card randomCard = DrawRandomCard();
                shuffledDeck.Add(randomCard);
            }

            _deck = shuffledDeck;
        }

        public List<Card> GetAllCards() => _deck;
    }
}