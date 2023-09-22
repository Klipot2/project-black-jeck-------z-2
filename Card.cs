﻿namespace Poker
{
    class Card
    {

        char rank;
        char suit;
 
        public Card(char rank, char suit)
        {
            this.rank = rank;
            this.suit = suit;          
        }
 
        public Card(Card card)
        {
            this.rank = card.rank;
            this.suit = card.suit;
        }
 
        public char GetRank()
        {
            return rank;
        }
 
        public char GetSuit()
        {
            return suit;
        }
    }
}