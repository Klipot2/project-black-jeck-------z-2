using System.Diagnostics;
using Casino.CardGames.Poker.Combinations;
using Castle.DynamicProxy.Contributors;

namespace Casino.CardGames.Poker
{
    public class FiveCardPoker : IPlayable
    {
        public const int MIN_BET = 10;
        private const int PLAYER_HAND_SIZE = 5;
        private const int CARD_SWAP_TERMINATOR = 0;
        private const int INITIAL_DEALER_LEEWAY = 0;
        private const int INITIAL_PLAYER_BANK = 100 * MIN_BET;
        private readonly int[] DEALER_BETS = [MIN_BET, MIN_BET*2, MIN_BET*3, MIN_BET*5, MIN_BET*8, MIN_BET*13, MIN_BET*21, MIN_BET*34, MIN_BET*55, MIN_BET*89];

        private readonly PokerHand _playerHand;
        private readonly PokerHand _dealerHand;
        private readonly DeckCards _deck;
        private int _tableBank;
        private bool _playerHasPassed = false;
        private int _dealerLeeway = INITIAL_DEALER_LEEWAY;

        private readonly List<int> _swapArray;
        private List<int> _possibleSwapInputs;

        public FiveCardPoker()
        {
            _playerHand = new PokerHand("Player", PLAYER_HAND_SIZE);
            _dealerHand = new PokerHand("Dealer", PLAYER_HAND_SIZE);
            _deck = new DeckCards();
            _playerHand.Bank = INITIAL_PLAYER_BANK;

            _swapArray = [];
            _possibleSwapInputs = [];
        }

        public void Play()
        {
            Console.WriteLine("Leeway:" + _dealerLeeway);
            _playerHasPassed = false;
            _deck.SetUpDeck();
            DealToDealer();
            DealToPlayer();
            _tableBank = 0;
            _playerHand.Bet = 0;
       
            _dealerHand.Bet = MIN_BET;
            _playerHand.MakeBet(MIN_BET);
            PokerUIHandler.NoResponseMessage(string.Format("Both dealer and you bet {0}.", MIN_BET));
            UpdateTableBank();

            DealerBetDecision();
            PokerUIHandler.DisplayPlayerStatus(_playerHand);
            PlayerBetQuery();
            if (!_playerHasPassed)
            {
                DealerCall();
                UpdateTableBank();
                
                PokerUIHandler.DisplayHand(_playerHand);
                PlayerCardSwapQuery();
                PokerUIHandler.DisplayPlayerStatus(_playerHand);

                PlayerSecondBetQuery();
                DealerCall();
                UpdateTableBank();

                bool playerHasBetterHand = _playerHand.Value > _dealerHand.Value;
                if (playerHasBetterHand)
                {
                    PokerUIHandler.NoResponseMessage("You won!");
                    _playerHand.Bank += _tableBank;
                    _dealerLeeway++;
                }
                else
                {
                    PokerUIHandler.NoResponseMessage("Dealer won!");
                    _dealerLeeway = INITIAL_DEALER_LEEWAY;
                }
            }   

            PokerUIHandler.NoResponseMessage("Your chip count after game: " + _playerHand.Bank);
            RestartQuery();
        }

        private void DealToDealer()
        {
            _dealerHand.ResetHand();
            List<Card> dealtCards = _deck.DrawCards(PLAYER_HAND_SIZE + _dealerLeeway);
            // DEBUG
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

            // DEBUG
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

        private void UpdateTableBank()
        {
            _tableBank = _dealerHand.Bet + _playerHand.Bet;
            PokerUIHandler.NoResponseMessage(string.Format("Current total bank: {0}", _tableBank));
        }

        //TODO: Dealer betting with RNG
        private void DealerBetDecision()
        {
            int valueBracket = _dealerHand.Value.GetValueBracket();
            if (valueBracket <= 0)
            {
                PokerUIHandler.NoResponseMessage("Dealer calls.");
                return;
            }
            _dealerHand.Bet = DEALER_BETS[valueBracket];
            PokerUIHandler.NoResponseMessage(string.Format("Dealer raises up to {0}.", _dealerHand.Bet));
        }

        private int GetBetDifference() => Math.Abs(_dealerHand.Bet - _playerHand.Bet);

        private void PlayerBetQuery()
        {
            string betDecisionMessage = "Enter a number, corresponding with your actions:\n1.Call\n2.Raise\n3.Pass";
            List<int> possibleNumericInputs = [1, 2, 3];
            PokerUIHandler.MessageWithNumericResponse(betDecisionMessage, possibleNumericInputs,
                "Press '1' to call, '2' to raise or '3' to pass.", PlayerBetDecision);
        }

        private void PlayerBetDecision(int inputInt)
        {
            const int CALL_NUM = 1;
            const int RAISE_NUM = 2;
            const int PASS_NUM = 3;

            int betDifference = GetBetDifference();
            switch (inputInt)
            {
                case CALL_NUM:
                    if (betDifference > _playerHand.Bank)
                    {
                        _playerHand.MakeBet(_playerHand.Bank);
                        PokerUIHandler.NoResponseMessage("You don't have enough to match dealer's bet, so you go all-in.");
                        return;
                    }
                    if (betDifference == 0)
                    {
                        PokerUIHandler.NoResponseMessage("You keep the bets as is.");
                        return;
                    }
                    _playerHand.MakeBet(betDifference);
                    PokerUIHandler.NoResponseMessage("You match dealer's bet.");
                    return;
                case RAISE_NUM:
                    if (betDifference >= _playerHand.Bank)
                    {
                        PokerUIHandler.NoResponseMessage("You don't have enough to raise. Make another choice.");
                        PlayerBetQuery();
                        return;
                    }

                    string inputBetPrompt = string.Format("Enter the amount you want to bet (should be more than {0}). {1} is the lowest bet step.", _dealerHand.Bet, MIN_BET);
                    int minRaise = _dealerHand.Bet + MIN_BET;
                    int maxRaise = _playerHand.Bank + _playerHand.Bet;
                    string fallbackBetPrompt = string.Format("Enter the number between {0} and {1} to bet.", minRaise, maxRaise);
                    PokerUIHandler.MessageWithRangeResponse(inputBetPrompt, minRaise, maxRaise, fallbackBetPrompt, ProcessPlayerBetRaise);
                    break;
                case PASS_NUM:
                    PokerUIHandler.NoResponseMessage("You passed. Dealer won by default.");
                    _playerHasPassed = true;
                    break;   
                default:
                    throw new ArgumentException(
                    string.Format("Got '{0}' as input, which is not acceptable for PlayerBetDecision.",
                    inputInt));
            }
        }

        private void ProcessPlayerBetRaise(int inputInt)
        {
            int amountToBet = inputInt / MIN_BET * MIN_BET;
            int betLeftovers = inputInt % MIN_BET;
            if (betLeftovers != 0)
            {
                string betLeftoverMessage = string.Format("Bet should be a multiple of {0}, so lowered your bet to {1}.", MIN_BET, amountToBet);
                PokerUIHandler.NoResponseMessage(betLeftoverMessage);
                _playerHand.MakeBet(amountToBet - _playerHand.Bet);
                return;
            }
            PokerUIHandler.NoResponseMessage(string.Format("You bet {0}.", amountToBet));
            _playerHand.MakeBet(amountToBet - _playerHand.Bet);
        }

        private void DealerCall()
        {
            int betDifference = GetBetDifference();
            if (betDifference == 0) return;
            _dealerHand.Bet += betDifference;
            PokerUIHandler.NoResponseMessage("Dealer matches your bet.");
        }

        private void PlayerCardSwapQuery()
        {
            Input[] possibleInputs = [Input.Yes, Input.No];
            PokerUIHandler.MessageWithInputResponse("Do you want to swap any cards? Press (Y)es or (N)o.",
                possibleInputs, "Press 'Y' to swap cards, or 'N' to skip.", LaunchCardSwap);
            foreach (var cardPosition in _swapArray)
            {
                Card newCard = _deck.DrawCard();
                _playerHand.PlainSwapCard(cardPosition - 1, newCard);
            }
            _playerHand.ForceUpdateHandValue();
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

        private void PlayerSecondBetQuery()
        {
            string betDecisionMessage = "Enter a number, corresponding with your actions:\n1.Call\n2.Raise";
            List<int> possibleNumericInputs = [1, 2];
            PokerUIHandler.MessageWithNumericResponse(betDecisionMessage, possibleNumericInputs,
                "Press '1' to call or '2' to raise.", PlayerBetDecision);
        }

        private void RestartQuery()
        {
            Input[] possibleInputs = [Input.Yes, Input.No];
            PokerUIHandler.MessageWithInputResponse("Do you want to play another game? Press (Y)es or (N)o.",
                possibleInputs, "Press 'Y' to start another game, or 'N' to close the program.", RestartSequence);
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
