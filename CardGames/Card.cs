namespace Casino.CardGames
{
    public class Card
    {
        /// <summary> Suit of a card.</summary>
        public enum Suit
        {
            S,
            H,
            C,
            D
        }

        /// <summary> Value of a card.</summary>
        public enum Value
        {
            Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace
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
