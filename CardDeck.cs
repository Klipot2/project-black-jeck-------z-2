namespace Poker
{
    class CardDeck
    {
        public enum AllSuits
        {
            S,  // Spades
            H,  // Hearts  
            C,  // Clubs
            D   // Diamonds
        }

        // Suggestion: move visuals to separate data class
        public enum AllRanks
        {
            Two = '2',
            Three = '3',
            Four = '4',
            Five = '5',
            Six = '6',
            Seven = '7',
            Eight = '8',
            Nine = '9',
            Ten = 'T',
            J = 'J', // Jack
            Q = 'Q', // Queen
            K = 'K', // King
            A = 'A'  // Ace
        }

        List<Card> cardDeck = new List<Card>();

        private void FillDeck()
        {
            foreach (var suit in Enum.GetValues(typeof(AllSuits)))
            {
                foreach (var rank in Enum.GetValues(typeof(AllRanks)))
                {
                    Card temporaryCard = new Card((char)rank, (char)suit);
                    cardDeck.Add(temporaryCard);
                }
            }
        }

        public CardDeck()
        {
            FillDeck();
			ShuffleDeck(cardDeck);
        }

        public void WriteDeck()
        {
            foreach (var card in cardDeck)
            {
                Console.WriteLine($"{card.Rank} of {card.Suit}");
            }
        }

        public Card GetCard()
        {
            Card card = cardDeck[0];
            cardDeck.RemoveAt(0);
            // Возвращаем первую карту из перемешанной колоды
            return card;
        }

        private void ShuffleDeck(List<Card> deck)
        {
            Random rng = new Random();
            int n = deck.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = deck[k];
                deck[k] = deck[n];
                deck[n] = value;
            }
        }
    }
}
