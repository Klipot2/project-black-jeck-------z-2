using System;
using System.Collections.Generic;

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
        }

        public void WriteDeck()
        {
            foreach (var card in cardDeck)
            {
                Console.WriteLine($"{card.GetRank()} of {card.GetSuit()}");
            }
        }

        public List<Card> GetShuffledDeck()
        {
            List<Card> shuffledDeck = new List<Card>(cardDeck);
            // реализовать логику тасования колоды.
            ShuffleDeck(shuffledDeck);
            return shuffledDeck;
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