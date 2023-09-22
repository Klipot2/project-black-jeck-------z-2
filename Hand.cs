namespace Poker
{
    class Hand
    {
        public List<Card> Cards { get { return _cardHand; } }
        public int CurrentSize { get { return _cardHand.Count; } }

        private int _handLimit;
        private List<Card> _cardHand = new List<Card>();

        public Hand(int handSizeLimit)
        {
            _handLimit = handSizeLimit;
        }

        public void AddCard(Card card)
        {
            if (CurrentSize >= _handLimit)
                throw new Exception("Cannot add card to hand due to reaching hand size limit.");
            _cardHand.Add(card);
        }

        public void AddCards(List<Card> cardList)
        {
            foreach (Card card in cardList)
            {
                AddCard(card);
            }
        }

        public void DrawCardsFromDeck(CardDeck deck, int amountToDraw)
        {
            for (int i = 0; i < amountToDraw; i++)
            {
                AddCard(deck.GetCard());
            }
        }

        //TODO: This logic needs to be handled by Poker-specific class
        public string EvaluateHand()
        {
            // Сортировка карт в руке по номиналу
            _cardHand.Sort((a, b) => a.Rank.CompareTo(b.Suit));

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
                return "Старшая карта: " + _cardHand.Last().ToString();
            }
        }

        //TODO: This logic needs to be handled by Poker-specific class
        private bool CheckForStraight()
        {
            // Проверка на стрит
            for (int i = 0; i < _cardHand.Count - 1; i++)
            {
                if (_cardHand[i].Rank + 1 != _cardHand[i + 1].Rank)
                {
                    return false;
                }
            }
            return true;
        }

        //TODO: This logic needs to be handled by Poker-specific class      
        private bool CheckForFlush()
        {
            // Проверка на флеш
            char suit = _cardHand[0].Suit;
            return _cardHand.All(card => card.Suit == suit);
        }

        //TODO: This logic needs to be handled by Poker-specific class
        private bool HasFourOfAKind()
        {
            // Проверка на каре (четыре карты одного номинала)
            for (int i = 0; i < _cardHand.Count - 3; i++)
            {
                if (_cardHand[i].Rank == _cardHand[i + 1].Rank &&
                    _cardHand[i].Rank == _cardHand[i + 2].Rank &&
                    _cardHand[i].Rank == _cardHand[i + 3].Rank)
                {
                    return true;
                }
            }
            return false;
        }

        //TODO: This logic needs to be handled by Poker-specific class
        private bool HasFullHouse()
        {
            // Проверка на фулл-хаус (тройка + пара)
            if (HasThreeOfAKind())
            {
                for (int i = 0; i < _cardHand.Count - 1; i++)
                {
                    if (_cardHand[i].Rank == _cardHand[i + 1].Rank &&
                        _cardHand[i].Rank != _cardHand[i + 2].Rank)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //TODO: This logic needs to be handled by Poker-specific class
        private bool HasThreeOfAKind()
        {
            // Проверка на тройку (три карты одного номинала)
            for (int i = 0; i < _cardHand.Count - 2; i++)
            {
                if (_cardHand[i].Rank == _cardHand[i + 1].Rank &&
                    _cardHand[i].Rank == _cardHand[i + 2].Rank)
                {
                    return true;
                }
            }
            return false;
        }

        //TODO: This logic needs to be handled by Poker-specific class
        private bool HasTwoPair()
        {
            // Проверка на две пары
            int pairCount = 0;
            for (int i = 0; i < _cardHand.Count - 1; i++)
            {
                if (_cardHand[i].Rank == _cardHand[i + 1].Rank)
                {
                    pairCount++;
                    i++; // Пропустить следующую карту, так как она уже в паре
                }
            }
            return pairCount == 2;
        }

        //TODO: This logic needs to be handled by Poker-specific class
        private bool HasPair()
        {
            // Проверка на пару (две карты одного номинала)
            for (int i = 0; i < _cardHand.Count - 1; i++)
            {
                if (_cardHand[i].Rank == _cardHand[i + 1].Rank)
                {
                    return true;
                }
            }
            return false;
        }
    }
}