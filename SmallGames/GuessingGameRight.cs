namespace Casino.SmallGames
{
    public class GuessingGameRight : IPlayable
    {
        private int userLimit;
        private int numberOfGuesses;
        private List<bool> guessList = new List<bool>();
        private InputAnswer inputAnswer = InputAnswer.Default;
        private bool exitOrUndo = false;

        private enum InputAnswer
        {
            Yes, No, Undo, Exit, Default
        }

        public void Play()
        {
            Console.WriteLine("Please input a positive number to limit the guessing space.");

            string? input = Console.ReadLine();
            if (input != null && (input.ToLower() == "q" || input.ToLower() == "z")) ExitGame();
            if (!int.TryParse(input, out userLimit))
            {
                Console.WriteLine("Input couldn't be recognized as a number.");
                Play();
                return;
            }
            if (userLimit <= 0)
            {
                Console.WriteLine("Your input was a non-positive number.");
                Play();
                return;
            }
            Console.WriteLine("\nGreat! Now think of a number within guessing space and I'll try to guess it!");
            numberOfGuesses = 0;
            guessList.Clear();
            Guess(0, userLimit);
        }

        private void Guess(int lowerLimit, int upperLimit, bool partlySkipGuess = false)
        {
            numberOfGuesses += 1;
            int newTry = (upperLimit + lowerLimit) / 2;
            if (!partlySkipGuess)
            {
                Console.WriteLine($"\nIs your number {newTry}? Answer (Y)es or (N)o.");

                exitOrUndo = ProcessInput(out inputAnswer);
                if (exitOrUndo) return;
                if (inputAnswer == InputAnswer.Yes)
                {
                    guessList.Add(true);
                    StartWinSequence();
                    return;
                }
                guessList.Add(false);
            }

            Console.WriteLine($"\nIs your number bigger than {newTry}?");
            exitOrUndo = ProcessInput(out inputAnswer);
            if (exitOrUndo) // TODO: Remove undefined behaviour
            {
                return;
            }
            else if (inputAnswer == InputAnswer.Yes)
            {
                guessList.Add(true);
                Guess(newTry, upperLimit);
            }
            else
            {
                guessList.Add(false);
                Guess(lowerLimit, newTry);
            }
        }

        private bool ProcessInput(out InputAnswer inputAnswer)
        {
            inputAnswer = ReadInput();
            if (inputAnswer == InputAnswer.Exit)
            {
                ExitGame();
                return true;
            }
            if (inputAnswer == InputAnswer.Undo)
            {
                UndoMove();
                return true;
            }
            return false;
        }

        private InputAnswer ReadInput()
        {
            bool inputRecognized = false;

            while (!inputRecognized)
            {
                string? input = Console.ReadLine();
                if (input == null)
                {
                    Console.WriteLine("Couldn't recognize input. Please try again.");
                    continue;
                }
                if (input.ToLower() == "y")
                {
                    return InputAnswer.Yes;
                }
                else if (input.ToLower() == "n")
                {
                    return InputAnswer.No;
                }
                else if (input.ToLower() == "z")
                {
                    return InputAnswer.Undo;
                }
                else if (input.ToLower() == "q")
                {
                    return InputAnswer.Exit;
                }
                else
                {
                    Console.WriteLine("Please, answer 'y', 'n', 'z' or 'q'.");
                }
            }
            return InputAnswer.Default;
        }

        private void StartWinSequence()
        {
            Console.WriteLine($"\nGuessed your number! Number of tries: {numberOfGuesses}. Want to try again?\n");
            exitOrUndo = ProcessInput(out inputAnswer);
            if (exitOrUndo) return;
            else if (inputAnswer == InputAnswer.Yes) Play();
            else ExitGame();
        }

        private void ExitGame()
        {
            Console.WriteLine("\nExitting game...");
            Environment.Exit(0);
        }

        private void UndoMove()
        {
            if (guessList.Count == 0)
            {
                Play();
            }
            else
            {
                guessList.RemoveAt(guessList.Count - 1);
                RecalculateGameState();
            }
        }

        private void RecalculateGameState()
        {
            numberOfGuesses = 0;
            int lowerLimit = 0;
            int upperLimit = userLimit;
            int i = 0;
            bool partlySkipGuess = false;
            foreach (var targetInTopBracket in guessList)
            {
                if (i % 2 != 0)
                {
                    numberOfGuesses += 1;
                    int partialGuess = (upperLimit + lowerLimit) / 2;
                    if (targetInTopBracket == true)
                    {
                        lowerLimit = partialGuess;
                    }
                    else
                    {
                        upperLimit = partialGuess;
                    }
                }
                partlySkipGuess = !partlySkipGuess;
                i++;
            }
            Guess(lowerLimit, upperLimit, partlySkipGuess);
        }
    }
}