using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public enum Hand
    {
        Nothing,
        OnePair,
        TwoPairs,
        ThreeKind,
        Straight,
        Flush,
        FullHouse,
        FourKind
    }
    public struct HandValue
    {
        public int Total { get; set; }
        public int HighCard { get; set; }
    }
    class HandEvaluator
    {
        private int heartsSum;
        private int diamondsSum;
        private int clubsSum;
        private int spadesSum;
        private Card[] cards;
        private HandValue handValue;

        public HandEvaluator(Card[] sortedHand, int betAmount)
        {
            heartsSum = 0;
            diamondsSum = 0;
            clubsSum = 0;
            spadesSum = 0;
            cards = new Card[5];
            Cards = sortedHand;
            handValue = new HandValue();
        }
        public HandValue HandValues
        {
            get { return handValue; }
            set { handValue = value; }
        }
        public Card[] Cards
        {
            get { return cards; }
            set
            {
                cards[0] = value[0];
                cards[1] = value[1];
                cards[2] = value[2];
                cards[3] = value[3];
                cards[4] = value[4];
            }
        }
        public Hand EvaluateHand()
        {
            getNumberOfSuit();
            if (FourKind())
                return Hand.FourKind;
            else if (FullHouse())
                return Hand.FullHouse;
            else if (Flush())
                return Hand.Flush;
            else if (Straight())
                return Hand.Straight;
            else if (ThreeKind())
                return Hand.ThreeKind;
            else if (TwoPairs())
                return Hand.TwoPairs;
            else if (OnePair())
                return Hand.OnePair;

            handValue.HighCard = (int)cards[4].CardValue;
            return Hand.Nothing;

        }
        private void getNumberOfSuit()
        {
            foreach (var element in Cards)
            {
                if (element.CardSuit == Card.Suit.H)
                    heartsSum++;
                else if (element.CardSuit == Card.Suit.D)
                    diamondsSum++;
                else if (element.CardSuit == Card.Suit.C)
                    clubsSum++;
                else if (element.CardSuit == Card.Suit.S)
                    spadesSum++;
            }
        }

        private bool FourKind()
        {
            //if the first 4 cards, add values of the 4 cards and card in the highest
            if (cards[0].CardValue == cards[1].CardValue && cards[0].CardValue == cards[2].CardValue && cards[0].CardValue == cards[3].CardValue)
            {
                handValue.Total = (int)cards[1].CardValue * 4;
                handValue.HighCard = (int)cards[4].CardValue;
                return true;
            }
            else if (cards[1].CardValue == cards[2].CardValue && cards[1].CardValue == cards[3].CardValue && cards[1].CardValue == cards[4].CardValue)
            {
                handValue.Total = (int)cards[1].CardValue * 4;
                handValue.HighCard = (int)cards[0].CardValue;
                return true;
            }
            return false;
        }
        private bool FullHouse()
        {
            if ((cards[0].CardValue == cards[1].CardValue && cards[0].CardValue == cards[2].CardValue && cards[3].CardValue == cards[4].CardValue) ||
                (cards[0].CardValue == cards[1].CardValue && cards[2].CardValue == cards[3].CardValue && cards[2].CardValue == cards[4].CardValue))
            {
                handValue.Total = (int)(cards[0].CardValue) + (int)(cards[1].CardValue) + (int)(cards[2].CardValue) + (int)(cards[3].CardValue) + (int)(cards[4].CardValue);
                return true;
            }
            return false;
        }
        private bool Flush()
        {
            if (heartsSum == 5 || diamondsSum == 5 || clubsSum == 5 || spadesSum == 5)
            {
                handValue.Total = (int)cards[4].CardValue;
                return true;
            }
            return false;
        }
        private bool Straight()
        {
            if (cards[0].CardValue + 1 == cards[1].CardValue &&
                cards[1].CardValue + 1 == cards[2].CardValue &&
                cards[2].CardValue + 1 == cards[3].CardValue &&
                cards[3].CardValue + 1 == cards[4].CardValue)
            {
                handValue.Total = (int)cards[4].CardValue;
                return true;
            }
            return false;
        }
        private bool ThreeKind()
        {
            if ((cards[0].CardValue == cards[1].CardValue) && cards[0].CardValue == cards[2].CardValue ||
                (cards[1].CardValue == cards[2].CardValue && cards[1].CardValue == cards[3].CardValue))
            {
                handValue.Total = (int)cards[2].CardValue * 3;
                handValue.HighCard = (int)cards[4].CardValue;
                return true;
            }
            else if (cards[2].CardValue == cards[3].CardValue && cards[2].CardValue == cards[4].CardValue)
            {
                handValue.Total = (int)cards[2].CardValue * 3;
                handValue.HighCard = (int)cards[1].CardValue;
                return true;
            }
            return false;
        }

        private bool TwoPairs()
        {
            if (cards[0].CardValue == cards[1].CardValue && cards[2].CardValue == cards[3].CardValue)
            {
                handValue.Total = ((int)cards[1].CardValue * 2) + ((int)cards[3].CardValue * 2);
                handValue.HighCard = (int)cards[4].CardValue;
                return true;
            }
            else if (cards[0].CardValue == cards[1].CardValue && cards[3].CardValue == cards[4].CardValue)
            {
                handValue.Total = ((int)cards[1].CardValue * 2) + ((int)cards[3].CardValue * 2);
                handValue.HighCard = (int)cards[2].CardValue;
                return true;
            }
            else if (cards[1].CardValue == cards[2].CardValue && cards[3].CardValue == cards[4].CardValue)
            {
                handValue.Total = ((int)cards[1].CardValue * 2) + ((int)cards[3].CardValue * 2);
                handValue.HighCard = (int)cards[0].CardValue;
                return true;
            }
            return false;
        }
        private bool OnePair()
        {
            if (cards[0].CardValue == cards[1].CardValue)
            {
                handValue.Total = (int)cards[0].CardValue * 2;
                handValue.HighCard = (int)cards[4].CardValue;
                return true;
            }
            else if (cards[1].CardValue == cards[2].CardValue)
            {
                handValue.Total = (int)cards[1].CardValue * 2;
                handValue.HighCard = (int)cards[4].CardValue;
                return true;
            }
            else if (cards[2].CardValue == cards[3].CardValue)
            {
                handValue.Total = (int)cards[2].CardValue * 2;
                handValue.HighCard = (int)cards[4].CardValue;
                return true;
            }
            else if (cards[3].CardValue == cards[4].CardValue)
            {
                handValue.Total = (int)cards[3].CardValue * 2;
                handValue.HighCard = (int)cards[2].CardValue;
                return true;
            }
            return false;
        }


    }

}