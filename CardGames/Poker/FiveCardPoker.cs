using System.Diagnostics;
using Casino.CardGames.Poker.Combinations;

namespace Casino.CardGames.Poker
{
    /// <summary>
    /// Represents a five-card poker game.
    /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="FiveCardPoker"/> class.
        /// </summary>
        public FiveCardPoker()
        {
            _playerHand = new PokerHand("Player", PLAYER_HAND_SIZE);
            _dealerHand = new PokerHand("Dealer", PLAYER_HAND_SIZE);
            _deck = new DeckCards();
            _playerBank = INITIAL_PLAYER_BANK;

            _swapArray = new List<int>();
            _possibleSwapInputs = new List<int>();
        }

        /// <summary>
        /// Plays a round of the five-card poker game.
        /// </summary>
        public void Play()
        {
            _deck.SetUpDeck();
            DealToDealer();
            DealToPlayer();

            int currentBet = MIN_BET;
            _playerBank -= currentBet;
            _tableBank = currentBet * 2;

            PokerUIHandler.DisplayPlayerStatus(_playerHand, _playerBank, currentBet);

            Input[] possibleInputs = { Input.Yes, Input.No };
            
            // Player decides whether to swap cards
            PokerUIHandler.MessageWithInputResponse("Do you want to swap any cards? Press (Y)es or (N)o.",
                possibleInputs, "Press 'Y' to swap cards, or 'N' to skip.", LaunchCardSwap);

            // Swap cards based on player's choice
            foreach (var cardPosition in _swapArray)
            {
                Card newCard = _deck.DrawCard();
                _playerHand.PlainSwapCard(cardPosition - 1, newCard);
            }

            _playerHand.ForceUpdateHandValue();
            PokerUIHandler.DisplayPlayerStatus(_playerHand, _playerBank, currentBet);

            // Player raises bet or not
            // Dealer response (if needed)
            bool playerHasBetterHand = _playerHand.Value > _dealerHand.Value;
            
            // Display game result
            if (playerHasBetterHand)
            {
                Console.WriteLine("You won!");
            }
            else
            {
                Console.WriteLine("Dealer won!");
            }

            // Ask if the player wants to play another game
            PokerUIHandler.MessageWithInputResponse("Do you want to play another game? Press (Y)es or (N)o.",
                possibleInputs, "Press 'Y' to start another game, or 'N' to close the program.", RestartSequence);
        }

        /// <summary>
        /// Deals cards to the dealer, evaluates the dealer's hand, and displays it.
        /// </summary>
        private void DealToDealer()
        {
            _dealerHand.ResetHand();
            List<Card> dealtCards = _deck.DrawCards(PLAYER_HAND_SIZE + DEALER_LEEWAY);

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

        /// <summary>
        /// Deals cards to the player.
        /// </summary>
        private void DealToPlayer()
        {
            _playerHand.ResetHand();
            List<Card> dealtCards = _deck.DrawCards(PLAYER_HAND_SIZE);
            _playerHand.AddCards(dealtCards);
        }

        /// <summary>
        /// Initiates the card swapping process based on the player's choice.
        /// </summary>
        /// <param name="input">The player's input.</param>
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

        /// <summary>
        /// Processes each step of the card swapping based on player's input.
        /// </summary>
        /// <param name="inputInt">The numeric input from the player.</param>
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

        /// <summary>
        /// Restarts the game if the player chooses to play again.
        /// </summary>
        /// <param name="input">The player's input.</param>
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
