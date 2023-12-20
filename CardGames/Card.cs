namespace Casino.CardGames
{
    public class Card
    {
        /// <summary> Suit of a card.</summary>
        public enum Suit
        {
            /// <summary> Spades.</summary>
            S,
            /// <summary> Hearts.</summary>
            H,
            C,  // Clubs
            D   // Diamonds
        }

        /// <summary> Value of a card.</summary>
        public enum Value
        {
            /// <summary> Two.</summary>
            Two,
            /// <summary> Three.</summary>
            Three,
            Four, Five, Six, Seven, Eight, Nine, Ten, 
            Jack, Queen, King, Ace 
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

        private static string SuitToString(Suit suit)
        {
            return suit switch
            {
                Suit.H => "Hearts",
                Suit.S => "Spades",
                Suit.C => "Clubs",
                Suit.D => "Diamonds",
                _ => throw new ArgumentException("Couldn't recognize card suit to convert it into string."),
            };
        }

        private static string ValueToString(Value value)
        {
            return value switch
            {
                Value.Two => "Two",
                Value.Three => "Three",
                Value.Four => "Four",
                Value.Five => "Five",
                Value.Six => "Six",
                Value.Seven => "Seven",
                Value.Eight => "Eight",
                Value.Nine => "Nine",
                Value.Ten => "Ten",
                Value.Jack => "Jack",
                Value.Queen => "Queen",
                Value.King => "King",
                Value.Ace => "Ace",
                _ => throw new ArgumentException("Couldn't recognize card value to convert it into string."),
            };
        }

        public override string ToString()
        {
            return ValueToString(_value) + " of " + SuitToString(_suit);
        }
    }
}