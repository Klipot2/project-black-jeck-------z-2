namespace Casino.CardGames
{
    /// <summary>
    /// Represents a standard playing card.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="Card"/> class.
    /// </remarks>
    /// <param name="suit">The suit of the card.</param>
    /// <param name="value">The value of the card.</param>
    public class Card(Card.Suit suit, Card.Value value)
    {
        /// <summary>
        /// Represents the suits of a standard deck of cards.
        /// </summary>
        public enum Suit
        {
            /// <summary>
            /// Spades.
            /// </summary>
            S,  
            /// <summary>
            /// Hearts.
            /// </summary>
            H,  
            /// <summary>
            /// Clubs.
            /// </summary>
            C,  
            /// <summary>
            /// Diamonds.
            /// </summary>
            D   
        }

        /// <summary>
        /// Represents the values of a standard deck of cards.
        /// </summary>
        public enum Value
        {
            /// <summary>
            /// Two.
            /// </summary>
            Two, 
            /// <summary>
            /// Three.
            /// </summary>
            Three, 
            /// <summary>
            /// Four.
            /// </summary>
            Four, 
            /// <summary>
            /// Five.
            /// </summary>
            Five, 
            /// <summary>
            /// Six.
            /// </summary>
            Six, 
            /// <summary>
            /// Seven.
            /// </summary>
            Seven, 
            /// <summary>
            /// Eight.
            /// </summary>
            Eight, 
            /// <summary>
            /// Nine.
            /// </summary>
            Nine, 
            /// <summary>
            /// Ten.
            /// </summary>
            Ten, 
            /// <summary>
            /// Jack.
            /// </summary>
            Jack, 
            /// <summary>
            /// Queen.
            /// </summary>
            Queen, 
            /// <summary>
            /// King.
            /// </summary>
            King, 
            /// <summary>
            /// Ace.
            /// </summary>
            Ace
        }

        /// <summary> 
        /// Gets the suit of the card.
        /// </summary>
        public Suit CardSuit { get { return _suit; } }

        /// <summary> 
        /// Gets the value of the card.
        /// </summary>
        public Value CardValue { get { return _value; } }

        private readonly Suit _suit = suit;
        private readonly Value _value = value;
    }
}
