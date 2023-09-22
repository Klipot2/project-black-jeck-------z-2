using System;
using System.Collections.Generic;

namespace Poker
{
    class Hand
    {
        List<Card> cardHand = new List<Card>();

        private void FillHand(CardDeck deck, int number)
        {
            for(int i = 0; i< number; i++){
               cardHand.Add(deck.GetCard()); 
            }
        }

        public Hand(CardDeck deck, int number)
        {
            FillHand(deck, number);			
        }

        public void AddCardToHand(Card card){
            cardHand.Add(card);
        }

        public void AddHandToHand(List<Card> cardList)
        {
            cardHand.AddRange(cardList);
        }
       
        // тут по идее метод для оценки стоимости (использовать когда рука = 7)
    }
}