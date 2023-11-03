using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class DeckCards
    {
        const int NUM_OF_CARDS = 52;
        private List<Card> deck;

        public DeckCards()
        {
            deck = new List<Card>(); // new Card[NUM_OF_CARDS];
        }
        public List<Card> getDeck { get { return deck; } }
        public void setUpDeck()
        {
            int i = 0;
            foreach (Card.Suit s in Enum.GetValues(typeof(Card.Suit)))
            {
                foreach (Card.Value v in Enum.GetValues(typeof(Card.Value)))
                {
                    Card cardToAdd = new(s, v);
                    deck.Add(cardToAdd);
                    i++;
                }

            }
            ShuffleCards();

        }
        public void ShuffleCards()
        {
            Random rand = new Random();
            Card temp;

            for (int shuffleTimes = 0; shuffleTimes < 1000; shuffleTimes++)
            {
                for (int i = 0; i < NUM_OF_CARDS; i++)
                {
                    int secondCardIndex = rand.Next(13);
                    temp = deck[i];
                    deck[i] = deck[secondCardIndex];
                    deck[secondCardIndex] = temp;

                }
            }
        }
    }
}