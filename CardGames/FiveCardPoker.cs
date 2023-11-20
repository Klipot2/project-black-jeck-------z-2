namespace Casino.CardGames
{
    public class FiveCardPoker : IPlayable
    {
        private const int PLAYER_HAND_SIZE = 5;

        private readonly Hand _playerHand;
        private readonly Hand _dealerHand;
        private readonly List<Hand> _allHands;
        private readonly DeckCards _deck;

        public FiveCardPoker()
        {
            _playerHand = new Hand("Player");
            _dealerHand = new Hand("Dealer");

            _allHands = new List<Hand>
            {
                _playerHand,
                _dealerHand
            };

            _deck = new DeckCards();
        }

        public void Play()
        {
            _deck.SetUpDeck();
            foreach (var hand in _allHands)
            {
                List<Card> dealtCards = _deck.DrawCards(PLAYER_HAND_SIZE);
                hand.ResetHand();
                hand.AddCards(dealtCards);
            }

            HandEvaluator.SortHand(_playerHand);
            //TODO: добавить отображение карт игрока
            Console.WriteLine("\n" + _playerHand.Owner + ":");
            CardVisualizerStub.VisualizeHand(_playerHand);
            Console.WriteLine();
        }
    }
}