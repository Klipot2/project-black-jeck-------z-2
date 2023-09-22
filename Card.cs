﻿namespace Poker
{
    class Card
    {
        private char _rank;
        private char _suit;

        public char Rank { get { return _rank; } }
        public char Suit { get { return _suit; } }
 
        public Card(char rank, char suit)
        {
            _rank = rank;
            _suit = suit;          
        }
    }
}