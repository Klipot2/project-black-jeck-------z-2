namespace Casino.CardGames
{
    /// <summary>
    /// Represents a standard playing card.
    /// </summary>
    public class Card
    {
        /// <summary> 
        /// Suit of a card.
        /// </summary>
        public enum Suit
        {
            S,  // Spades
            H,  // Hearts
            C,  // Clubs
            D   // Diamonds
        }

        /// <summary> 
        /// Value of a card.
        /// </summary>
        public enum Value
        {
            Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace
        }

        /// <summary> 
        /// Gets the suit of the card.
        /// </summary>
        public Suit CardSuit { get { return _suit; } }

        /// <summary> 
        /// Gets the value of the card.
        /// </summary>
        public Value CardValue { get { return _value; } }

        private readonly Suit _suit;
        private readonly Value _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> class.
        /// </summary>
        /// <param name="suit">The suit of the card.</param>
        /// <param name="value">The value of the card.</param>
        public Card(Suit suit, Value value)
        {
            _suit = suit;
            _value = value;
        }
    }
}
