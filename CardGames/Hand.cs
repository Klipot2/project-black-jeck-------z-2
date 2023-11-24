namespace Casino.CardGames
{
    public class Hand
    {
        public int Size { get { return _hand.Count; } }
        public string Owner { get { return _owner; } }

        private List<Card> _hand;

        private string _owner;

        public Hand(string ownerName)
        {
            _hand = new List<Card>();
            _owner = ownerName;
        }

        public void AddCard(Card card)
        {
            _hand.Add(card);
        }

        public void AddCards(List<Card> cards)
        {
            foreach (var card in cards)
            {
                AddCard(card);
            }
        }

        public void SwapCard(int previousCardPosition, Card newCard)
        {
            _hand[previousCardPosition] = newCard;
        }

        public void ResetHand()
        {
            _hand = new List<Card>();
        }

        public List<Card> GetCards() => _hand;
    }
}