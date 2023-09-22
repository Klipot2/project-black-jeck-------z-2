using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker
{
    class Hand
    {
        List<Card> cardHand = new List<Card>();

        private void FillHand(CardDeck deck, int number)
        {
            for (int i = 0; i < number; i++)
            {
                cardHand.Add(deck.GetCard());
            }
        }

        public Hand(CardDeck deck, int number)
        {
            FillHand(deck, number);
        }

        public void AddCardToHand(Card card)
        {
            cardHand.Add(card);
        }

        public void AddHandToHand(List<Card> cardList)
        {
            cardHand.AddRange(cardList);
        }

        public string EvaluateHand()
        {
            // Сортировка карт в руке по номиналу
            cardHand.Sort((a, b) => a.GetRank().CompareTo(b.GetRank()));

            // Проверка на стрит
            bool isStraight = CheckForStraight();

            // Проверка на флеш
            bool isFlush = CheckForFlush();

            // Проверка на комбинации
            if (isStraight && isFlush)
            {
                return "Стрит-флеш";
            }
            else if (isStraight)
            {
                return "Стрит";
            }
            else if (isFlush)
            {
                return "Флеш";
            }
            else if (HasFourOfAKind())
            {
                return "Каре";
            }
            else if (HasFullHouse())
            {
                return "Фулл-хаус";
            }
            else if (HasThreeOfAKind())
            {
                return "Тройка";
            }
            else if (HasTwoPair())
            {
                return "Две пары";
            }
            else if (HasPair())
            {
                return "Пара";
            }
            else
            {
                // Если не найдено ни одной комбинации, вернуть старшую карту
                return "Старшая карта: " + cardHand.Last().ToString();
            }
        }

        private bool CheckForStraight()
        {
            // Проверка на стрит
            for (int i = 0; i < cardHand.Count - 1; i++)
            {
                if (cardHand[i].GetRank() + 1 != cardHand[i + 1].GetRank())
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckForFlush()
        {
            // Проверка на флеш
            char suit = cardHand[0].GetSuit();
            return cardHand.All(card => card.GetSuit() == suit);
        }

        private bool HasFourOfAKind()
        {
            // Проверка на каре (четыре карты одного номинала)
            for (int i = 0; i < cardHand.Count - 3; i++)
            {
                if (cardHand[i].GetRank() == cardHand[i + 1].GetRank() &&
                    cardHand[i].GetRank() == cardHand[i + 2].GetRank() &&
                    cardHand[i].GetRank() == cardHand[i + 3].GetRank())
                {
                    return true;
                }
            }
            return false;
        }

        private bool HasFullHouse()
        {
            // Проверка на фулл-хаус (тройка + пара)
            if (HasThreeOfAKind())
            {
                for (int i = 0; i < cardHand.Count - 1; i++)
                {
                    if (cardHand[i].GetRank() == cardHand[i + 1].GetRank() &&
                        cardHand[i].GetRank() != cardHand[i + 2].GetRank())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool HasThreeOfAKind()
        {
            // Проверка на тройку (три карты одного номинала)
            for (int i = 0; i < cardHand.Count - 2; i++)
            {
                if (cardHand[i].GetRank() == cardHand[i + 1].GetRank() &&
                    cardHand[i].GetRank() == cardHand[i + 2].GetRank())
                {
                    return true;
                }
            }
            return false;
        }

        private bool HasTwoPair()
        {
            // Проверка на две пары
            int pairCount = 0;
            for (int i = 0; i < cardHand.Count - 1; i++)
            {
                if (cardHand[i].GetRank() == cardHand[i + 1].GetRank())
                {
                    pairCount++;
                    i++; // Пропустить следующую карту, так как она уже в паре
                }
            }
            return pairCount == 2;
        }

        private bool HasPair()
        {
            // Проверка на пару (две карты одного номинала)
            for (int i = 0; i < cardHand.Count - 1; i++)
            {
                if (cardHand[i].GetRank() == cardHand[i + 1].GetRank())
                {
                    return true;
                }
            }
            return false;
        }
    }
}