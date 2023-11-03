using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker
{
    class DiceGame
    {
        private Random rand = new Random();

        public void Throw()
        {
            InitializeThrow();

            int numDice = GetNumberFromUser("Введите количество кубиков: ");
            int numSides = GetNumberFromUser("Введите количество граней на кубике: ");

            for (int i = 0; i < numDice; i++)
            {
                int diceResult = GetDieRoll(numSides);
                Console.WriteLine("Выпало число на кубике " + (i + 1) + ": " + diceResult);
            }

            EndThrow();
        }

        private void InitializeThrow()
        {
            Console.WriteLine("Добро пожаловать в игру с кубиками!");
            Console.WriteLine("Нажмите любую клавишу, чтобы бросить кубики...");
            Console.ReadKey();
        }

        private void EndThrow()
        {
            Console.WriteLine("Игра завершена.");
            Console.ReadKey();
        }

        private int GetDieRoll(int numSides)
        {
            return rand.Next(1, numSides + 1);
        }

        private int GetNumberFromUser(string message)
        {
            int number;
            bool isValidInput = false;

            do
            {
                Console.Write(message);
                isValidInput = int.TryParse(Console.ReadLine(), out number);

                if (!isValidInput)
                {
                    Console.WriteLine("Неверный ввод. Попробуйте снова.");
                }
            } while (!isValidInput);

            return number;
        }
    }
}