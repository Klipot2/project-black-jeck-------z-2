using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class Card
    {
        public enum SUIT
        {
            S,  // Spades
            H,  // Hearts  
            C,  // Clubs
            D   // Diamonds
        }
        public enum VALUE
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
            Jack = 'J', // Jack
            Queen = 'Q', // Queen
            King = 'K', // King
            Ace = 'A'  // Ace
        }
        public SUIT MySuit {get; set;}
        public VALUE MyValue {get; set;}
    }
}