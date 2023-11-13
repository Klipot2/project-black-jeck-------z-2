namespace Casino.CardGames
{
    public class Hand
    {
        public int Size { get { return _hand.Count; } }

        private List<Card> _hand;

        public Hand()
        {
            _hand = new List<Card>();
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

        public void ResetHand()
        {
            _hand = new List<Card>();
        }

        public List<Card> GetCards() => _hand;
    }
}