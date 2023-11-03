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
        public int Total {get; set;}
        public int HighCard {get; set;}
    }
    class HandEvaluator : Card
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
            get {return handValue;}
            set {handValue = value; }
        }
        public Card [] Cards
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

            handValue.HighCard = (int)cards[4].MyValue;
            return Hand.Nothing;

        }
        private void getNumberOfSuit()
        {
            foreach (var element in Cards)
            {
                if (element.MySuit == Card.SUIT.H)
                    heartsSum++;
                else if (element.MySuit == Card.SUIT.D)
                    diamondsSum++;
                else if (element.MySuit == Card.SUIT.C)
                    clubsSum++;
                else if (element.MySuit == Card.SUIT.S)
                    spadesSum++;
            }
        }

        private bool FourKind()
        {
            //if the first 4 cards, add values of the 4 cards and card in the highest
            if (cards[0].MyValue == cards[1].MyValue && cards[0].MyValue == cards[2].MyValue && cards[0].MyValue == cards[3].MyValue)
            {
                handValue.Total = (int)cards[1].MyValue * 4;
                handValue.HighCard = (int)cards[4].MyValue;
                return true;
            }
            else if (cards[1].MyValue == cards[2].MyValue && cards[1].MyValue == cards[3].MyValue && cards[1].MyValue == cards[4].MyValue)
            {
                handValue.Total = (int)cards[1].MyValue * 4;
                handValue.HighCard = (int)cards[0].MyValue;
                return true;                
            }
            return false;
        }
        private bool FullHouse()
        {
            if ((cards[0].MyValue == cards[1].MyValue && cards[0].MyValue == cards[2].MyValue && cards[3].MyValue == cards[4].MyValue) ||
                (cards[0].MyValue == cards[1].MyValue && cards[2].MyValue == cards[3].MyValue && cards[2].MyValue == cards[4].MyValue))
            {
                handValue.Total = (int)(cards[0].MyValue) + (int)(cards[1].MyValue) + (int)(cards[2].MyValue) + (int)(cards[3].MyValue) + (int)(cards[4].MyValue);
                return true;                          
            }
            return false;
        }
        private bool Flush()
        {
            if (heartsSum == 5 || diamondsSum == 5 || clubsSum == 5 || spadesSum == 5)
            {
                handValue.Total = (int)cards[4].MyValue;
                return true;
            }
            return false;
        }
        private bool Straight()
        {
            if (cards[0].MyValue + 1 == cards[1].MyValue &&
                cards[1].MyValue + 1 == cards[2].MyValue &&
                cards[2].MyValue + 1 == cards[3].MyValue &&
                cards[3].MyValue + 1 == cards[4].MyValue)
                {
                    handValue.Total = (int)cards[4].MyValue;
                    return true;
                }
                return false;
        }
        private bool ThreeKind()
        {
            if ((cards[0].MyValue == cards[1].MyValue) && cards[0].MyValue == cards[2].MyValue ||
                (cards[1].MyValue == cards[2].MyValue && cards[1].MyValue == cards[3].MyValue))
                {
                    handValue.Total = (int)cards[2].MyValue * 3;
                    handValue.HighCard = (int)cards[4].MyValue;
                    return true;
                }
                else if (cards[2].MyValue == cards[3].MyValue && cards[2].MyValue == cards[4].MyValue)
                {
                    handValue.Total = (int)cards[2].MyValue * 3;
                    handValue.HighCard = (int)cards[1].MyValue;
                    return true;
                }
                return false;            
        }
        
        private bool TwoPairs()
        {
            if (cards[0].MyValue == cards[1].MyValue && cards[2].MyValue == cards[3].MyValue)
            {
                handValue.Total = ((int)cards[1].MyValue * 2) + ((int)cards[3].MyValue *2);
                handValue.HighCard = (int)cards[4].MyValue;
                return true;
            }
            else if (cards[0].MyValue == cards[1].MyValue && cards[3].MyValue == cards[4].MyValue)
            {
                handValue.Total = ((int)cards[1].MyValue * 2) + ((int)cards[3].MyValue *2);
                handValue.HighCard = (int)cards[2].MyValue;
                return true;
            }
            else if (cards[1].MyValue == cards[2].MyValue && cards[3].MyValue == cards[4].MyValue)
            {
                handValue.Total = ((int)cards[1].MyValue * 2) + ((int)cards[3].MyValue *2);
                handValue.HighCard = (int)cards[0].MyValue;
                return true;
            }
            return false;
        }
        private bool OnePair()
        {
            if (cards[0].MyValue == cards[1].MyValue)
            {
                handValue.Total = (int)cards[0].MyValue * 2;
                handValue.HighCard = (int)cards[4].MyValue;
                return true;
            }
            else if (cards[1].MyValue == cards[2].MyValue)
            {
                handValue.Total = (int)cards[1].MyValue * 2;
                handValue.HighCard = (int)cards[4].MyValue;
                return true;
            }
            else if (cards[2].MyValue == cards[3].MyValue)
            {
                handValue.Total = (int)cards[2].MyValue * 2;
                handValue.HighCard = (int)cards[4].MyValue;
                return true;
            }
            else if (cards[3].MyValue == cards[4].MyValue)
            {
                handValue.Total = (int)cards[3].MyValue * 2;
                handValue.HighCard = (int)cards[2].MyValue;
                return true;
            }
            return false;
        }


    }

}