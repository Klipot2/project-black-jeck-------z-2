using System.Diagnostics;
using Casino.CardGames.Poker.Combinations;

namespace Casino.CardGames.Poker
{
    public class FiveCardPoker : IPlayable
    {
        private const int PLAYER_HAND_SIZE = 5;
        private const int CARD_SWAP_TERMINATOR = 0;
        private const int DEALER_LEEWAY = 2;
        private const int MIN_BET = 10;
        private const int INITIAL_PLAYER_BANK = 1000;


        private readonly PokerHand _playerHand;
        private readonly PokerHand _dealerHand;
        private readonly DeckCards _deck;
        private int _playerBank;
        private int _tableBank;

        private readonly List<int> _swapArray;
        private List<int> _possibleSwapInputs;

        public FiveCardPoker()
        {
            _playerHand = new PokerHand("Player", PLAYER_HAND_SIZE);
            _dealerHand = new PokerHand("Dealer", PLAYER_HAND_SIZE);
            _deck = new DeckCards();
            _playerBank = INITIAL_PLAYER_BANK; 


            _swapArray = [];
            _possibleSwapInputs = [];
        }

        public void Play()
        {
            _deck.SetUpDeck();
            DealToDealer();
            DealToPlayer();

            int currentBet = MIN_BET;
            _playerBank -= currentBet;
            _tableBank = currentBet * 2;
            

            PokerUIHandler.DisplayPlayerStatus(_playerHand, _playerBank, currentBet);
            // Dealer decides if they want to raise
            // Player response
            // Dealer response (if needed)
            
            Input[] possibleInputs = [Input.Yes, Input.No];
            PokerUIHandler.MessageWithInputResponse("Do you want to swap any cards? Press (Y)es or (N)o.",
                possibleInputs, "Press 'Y' to swap cards, or 'N' to skip.", LaunchCardSwap);
            foreach (var cardPosition in _swapArray)
            {
                Card newCard = _deck.DrawCard();
                // CardRenderer.PrintCards([_playerHand.GetCards()[cardPosition - 1], newCard]);
                _playerHand.PlainSwapCard(cardPosition - 1, newCard);
            }
            _playerHand.ForceUpdateHandValue();
            PokerUIHandler.DisplayPlayerStatus(_playerHand, _playerBank, currentBet);

            // Player raises bet or not
            // Dealer response (if needed)

            bool playerHasBetterHand = _playerHand.Value > _dealerHand.Value;
            if (playerHasBetterHand)
            {
                Console.WriteLine("You won!");
            }
            else
            {
                Console.WriteLine("Dealer won!");
            }

            PokerUIHandler.MessageWithInputResponse("Do you want to play another game? Press (Y)es or (N)o.",
                possibleInputs, "Press 'Y' to start another game, or 'N' to close the program.", RestartSequence);
        }

        private void DealToDealer()
        {
            _dealerHand.ResetHand();
            List<Card> dealtCards = _deck.DrawCards(PLAYER_HAND_SIZE + DEALER_LEEWAY);
            // List<Card> dealtCards =
            // [
            //     new Card(Card.Suit.D, Card.Value.Queen),
            //     new Card(Card.Suit.D, Card.Value.Jack),
            //     new Card(Card.Suit.D, Card.Value.King),
            //     new Card(Card.Suit.C, Card.Value.Nine),
            //     new Card(Card.Suit.C, Card.Value.Ace),
            //     new Card(Card.Suit.D, Card.Value.Ace),
            //     new Card(Card.Suit.C, Card.Value.Six),
            //     new Card(Card.Suit.D, Card.Value.Ten),
            //     new Card(Card.Suit.C, Card.Value.Four),
            //     new Card(Card.Suit.C, Card.Value.Jack),
            //     new Card(Card.Suit.C, Card.Value.Two),
            //     new Card(Card.Suit.C, Card.Value.Three),
            // ];
            ValueData expandedHandValue = new(dealtCards.Count, true);
            HandEvaluator.CalculateHandValueData(dealtCards, expandedHandValue);
            _dealerHand.AddCards(dealtCards.GetRange(0, PLAYER_HAND_SIZE));
            for (int i = PLAYER_HAND_SIZE; i < dealtCards.Count; i++)
            {
                _deck.AddCard(dealtCards[i]);
            }
            _deck.ShuffleCards();

            PokerUIHandler.DisplayHand(_dealerHand);
        }

        private void DealToPlayer()
        {
            _playerHand.ResetHand();
            List<Card> dealtCards = _deck.DrawCards(PLAYER_HAND_SIZE);
            // List<Card> dealtCards =
            // [
            //     new Card(Card.Suit.C, Card.Value.Jack),
            //     new Card(Card.Suit.C, Card.Value.Five),
            //     new Card(Card.Suit.H, Card.Value.Four),
            //     new Card(Card.Suit.D, Card.Value.Eight),
            //     new Card(Card.Suit.D, Card.Value.Ace)
            // ];
            _playerHand.AddCards(dealtCards);
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

                    PokerUIHandler.MessageWithNumericResponse(initialSwapMessage, _possibleSwapInputs,
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

            if (!_swapArray.Remove(inputInt))
            {
                _swapArray.Add(inputInt);
            }

            string initialSwapMessage = string.Format("{0} Choose another card to swap, or press '{1}' to stop. To undo swap, type card number again.",
                PokerUIHandler.SwapArrayToString(_swapArray), CARD_SWAP_TERMINATOR);
            string fallbackSwapMessage = string.Format(
                "{0} Select a new card to swap, undo swap or press '{1}' to stop.",
                PokerUIHandler.SwapArrayToString(_swapArray), CARD_SWAP_TERMINATOR);

            PokerUIHandler.MessageWithNumericResponse(initialSwapMessage, _possibleSwapInputs,
                fallbackSwapMessage, ProcessCardSwapStep);
        }

        private void RestartSequence(Input input)
        {
            switch (input)
            {
                case Input.Yes:
                    Play();
                    break;
                case Input.No:
                    Console.WriteLine("Closing the game...");
                    break;
                default:
                    throw new ArgumentException(
                    string.Format("Cannot recognize {0} input for RestartSequence.",
                    input));
            }
        }
    }
}
