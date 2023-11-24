namespace Casino.CardGames
{
    public class FiveCardPoker : IPlayable
    {
        private const int PLAYER_HAND_SIZE = 5;
        private const int CARD_SWAP_TERMINATOR = 0;

        private readonly Hand _playerHand;
        private readonly Hand _dealerHand;
        private readonly List<Hand> _allHands;
        private readonly DeckCards _deck;

        private readonly List<int> _swapArray;
        private List<int> _possibleSwapInputs;

        public FiveCardPoker()
        {
            _playerHand = new Hand("Player");
            _dealerHand = new Hand("Dealer");

            _allHands = new List<Hand>
            {
                _playerHand,
                _dealerHand
            };

            _deck = new();

            _swapArray = new();
            _possibleSwapInputs = new();
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
            Console.WriteLine();
            UIHandler.DisplayHand(_playerHand);
            Input[] possibleInputs = { Input.Yes, Input.No };
            UIHandler.MessageWithInputResponse("Do you want to swap any cards? Press (Y)es or (N)o.",
                possibleInputs, "Press 'Y' to swap cards, or 'N' to skip.", LaunchCardSwap);  
            foreach (var cardPosition in _swapArray)
            {
                Card newCard = _deck.DrawCard();
                _playerHand.SwapCard(cardPosition - 1, newCard);
            }
            UIHandler.DisplayHand(_playerHand); 
        }

        private void LaunchCardSwap(Input input)
        {
            switch (input)
            {
                case Input.Yes:
                    _swapArray.Clear();
                    _possibleSwapInputs = Enumerable.Range(0, PLAYER_HAND_SIZE + 1).ToList();
                    _possibleSwapInputs[0] = CARD_SWAP_TERMINATOR;

                    string swapMessageBody = string.Format("a number between {0} and {1} or '{2}' to stop.",
                        1, PLAYER_HAND_SIZE, CARD_SWAP_TERMINATOR);
                    string initialSwapMessage = "Which card do you want to swap first? Enter " + swapMessageBody;
                    string fallbackSwapMessage = "To swap card enter " + swapMessageBody;

                    UIHandler.MessageWithNumericResponse(initialSwapMessage, _possibleSwapInputs,
                        fallbackSwapMessage, ProcessCardSwapStep);
                    break;
                case Input.No:
                    break;
                default:
                    throw new ArgumentException(
                    string.Format("Cannot recognize {0} input for LaunchCardSwap.",
                    input));
            }
        }

        private void ProcessCardSwapStep(int inputInt)
        {
            if (inputInt == CARD_SWAP_TERMINATOR) return;

            if (inputInt < 1 || inputInt > PLAYER_HAND_SIZE)
                throw new ArgumentOutOfRangeException(
                string.Format("Input for CardSwap is not {0} and not between 1 and {1}.",
                CARD_SWAP_TERMINATOR, PLAYER_HAND_SIZE));

            _swapArray.Add(inputInt);
            _possibleSwapInputs.Remove(inputInt);

            string initialSwapMessage = string.Format("{0} Choose another card to swap, or press '{1}' to stop.",
                UIHandler.SwapArrayToString(_swapArray), CARD_SWAP_TERMINATOR);
            string fallbackSwapMessage = string.Format(
                "{0} Select a card that has not been swapped already, or press '{1}' to stop.",
                UIHandler.SwapArrayToString(_swapArray), CARD_SWAP_TERMINATOR);

            UIHandler.MessageWithNumericResponse(initialSwapMessage, _possibleSwapInputs,
                fallbackSwapMessage, ProcessCardSwapStep);
        }
    }
}