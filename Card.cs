namespace Poker
{
    class Card
    {
        public enum Suit
        {
            S,  // Spades
            H,  // Hearts  
            C,  // Clubs
            D   // Diamonds
        }

        public enum Value
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

        public Suit CardSuit { get { return _suit; } }
        public Value CardValue { get { return _value; } }

        private readonly Suit _suit;
        private readonly Value _value;

        public Card(Suit suit, Value value)
        {
            _suit = suit;
            _value = value;
        }
    }
}