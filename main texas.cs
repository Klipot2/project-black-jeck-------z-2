using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
 
namespace Poker
{
    class Game
    {
        IList<Card> cards;
        ICollection<Card> cardsCopy;
        const int deckSize = 52;
        const int suitSize = 4;
        const int rankSize = deckSize / suitSize;
        const int cardsInHand = 5;
        Dictionary<int, char> rankMap;
        Dictionary<int, char> suitMap;
        IList<MethodInfo> combinations;
        Dictionary<int, int> rankOutput;
        Dictionary<char, int> suitOutput;
        Random random = new Random();
 
        public Game()
        {          
            InitializeRankMap();
            InitializeSuitMap();
            InitializeDeck();
 
            InitializeCombinations();
        }
 
        private void InitializeDeck()
        {
            cards = new List<Card>();
            cardsCopy = new HashSet<Card>();
 
            foreach (char rank in rankMap.Values)
            {
                foreach (char suit in suitMap.Values)
                {
                    Card card = new Card(rank, suit);
                    cards.Add(card);
                    cardsCopy.Add(card);
                }
            }
        }
 
        private void InitializeRankMap()
        {
            rankMap = new Dictionary<int, char>();
            
            for (int i = 1; i < 9; i++)
            {
                rankMap.Add(i, (i + 1).ToString().ToCharArray().First());
            }
 
            rankMap.Add(9, 'T');
            rankMap.Add(10, 'J');
            rankMap.Add(11, 'Q');
            rankMap.Add(12, 'K');
            rankMap.Add(13, 'A');
        }
 
        private void InitializeSuitMap()
        {
            suitMap = new Dictionary<int, char>();
            suitMap.Add(0, 'S');
            suitMap.Add(1, 'H');
            suitMap.Add(2, 'C');
            suitMap.Add(3, 'D');
        }
 
        public ICollection<Card> DealCards()
        {
            var cardsToDeal = new HashSet<Card>();
 
            for (int i = 0; i < cardsInHand; i++)
            {
                int index = random.Next(0, cards.Count);
 
                if (index > cards.Count - 1)
                {
                    throw new IndexOutOfRangeException();
                }
 
                Card card = new Card(cards[index].Rank,cards[index].Suit);
                cardsToDeal.Add(card);
 
                cards.RemoveAt(index);
            }
 
            return cardsToDeal;
        }
 
        public Tuple<bool, string> GetHandSummary(ICollection<Card> playerCards)
        {
            if (playerCards.Count != cardsInHand)
            {
                throw new ArgumentOutOfRangeException();
            }
 
            rankOutput = CalculateRankOutput(playerCards);
            suitOutput = CalculateSuitOutput(playerCards);
 
            foreach (var combination in combinations)
            {
                var tuple = (Tuple<bool, string>)combination.Invoke(this, new object[] { playerCards });
 
                if (tuple.Item1)
                {
                    return tuple;
                }
            }
 
            return new Tuple<bool, string>(true, "High Card");
        }
 
        private void InitializeCombinations()
        {            
            var methodInfoList = typeof(Game).GetMethodsBySignature(typeof(Tuple<bool, string>), typeof(ICollection<Card>));
            combinations = new List<MethodInfo>(methodInfoList);
        }
 
        private Tuple<bool, string> IsStraightFlush(ICollection<Card> hand)
        {
            bool isStraight = HasStraight(hand).Item1;
            bool isFlush = HasFlush(hand).Item1;
            bool isRoyalFlush = rankOutput.Min(o => o.Key) == 9;
 
            bool isStraightFlush = isStraight && isFlush;
            string name = isStraightFlush && isRoyalFlush ? "Royal Flush" : "Straight Flush";
 
            return Tuple.Create(isStraightFlush, name);
        }
 
        private Tuple<bool, string> IsFourOfKind(ICollection<Card> hand)
        {
            bool isFourOfKind = rankOutput.Where(r => r.Value == 4).Count() == 1;
            return Tuple.Create(isFourOfKind, "Four of a Kind");
        }
 
        private Tuple<bool, string> IsFullHouse(ICollection<Card> hand)
        {
            bool hasThreeOfKind = HasThreeOfKind(hand).Item1;
            bool hasPair = HasPair(hand).Item1;
 
            return Tuple.Create(hasThreeOfKind && hasPair, "Full House");
        }
 
        private Tuple<bool, string> HasFlush(ICollection<Card> hand)
        {
            bool hasFlush = suitOutput.Max(s => s.Value) == 5;
           // bool isStraight = IsStraight(hand).Item1;
 
            return Tuple.Create(hasFlush /*&& !isStraight*/, "Flush");
        }
 
        private Tuple<bool, string> HasStraight(ICollection<Card> hand)
        {
            var rankOutputCopy = new Dictionary<int, int>(rankOutput);
 
            if (rankOutputCopy.Keys.Contains(2) && rankOutputCopy.Keys.Contains(13))
            {
                int value = rankOutputCopy[13];
                rankOutputCopy.Remove(13);
                rankOutputCopy[0] = value;
            }
 
            int minRank = rankOutputCopy.Min(o => o.Key);
            int maxRank = rankOutputCopy.Max(o => o.Key);
 
            bool hasStraight = maxRank - minRank == 4;
            bool hasPair = HasPair(hand).Item1;
            bool hasTwoPair = HasTwoPair(hand).Item1;
            bool hasSet = HasThreeOfKind(hand).Item1;
            //bool hasFlush = HasFlush(hand).Item1;
 
            return Tuple.Create(hasStraight && !hasPair && !hasSet && !hasTwoPair /*&& !hasFlush*/, "Straight");
        }
 
        private Tuple<bool, string> HasThreeOfKind(ICollection<Card> hand)
        {
            bool hasThreeOfKind = rankOutput.Where(r => r.Value == 3).Count() == 1;
            //bool hasFlush = HasFlush(hand).Item1;
            //bool hasPair = HasOnePair(hand).Item1;
 
            return Tuple.Create(hasThreeOfKind /*&& !hasFlush && !hasPair*/, "Three of a Kind");
        }
 
        private Tuple<bool, string> HasTwoPair(ICollection<Card> hand)
        {
            bool hasTwoPair = rankOutput.Where(r => r.Value == 2).Count() == 2;
            //bool hasFlush = HasFlush(hand).Item1;
 
            return Tuple.Create(hasTwoPair /*&& !hasFlush*/, "Two Pair");
        }
 
        private Tuple<bool, string> HasPair(ICollection<Card> hand)
        {
            bool hasOnePair = rankOutput.Where(r => r.Value == 2).Count() == 1;
            //bool hasTwoPair = HasTwoPair(hand).Item1;
            //bool hasThreeOfKind = HasThreeOfKind(hand).Item1;
            //bool hasFlush = HasFlush(hand).Item1;
 
            return Tuple.Create(hasOnePair /*&& !isTwoPair && !isThreeOfKind && !isFlush*/, "One Pair");
        }
 
        private Dictionary<int, int> CalculateRankOutput(ICollection<Card> playerCards)
        {
            var output = new Dictionary<int, int>();
 
            foreach (Card card in playerCards)
            {
                int rank = rankMap.FirstOrDefault(x => x.Value == card.Rank).Key;
                int value;
                output.TryGetValue(rank, out value);
 
                output[rank] = value == 0 ? 1 : output[rank] + 1;
            }
 
            return output;
        }
 
        private Dictionary<char, int> CalculateSuitOutput(ICollection<Card> playerCards)
        {
            var output = new Dictionary<char, int>();
 
            foreach (Card card in playerCards)
            {
                char suit = card.Suit;
                int value;
                output.TryGetValue(suit, out value);
 
                output[suit] = value == 0 ? 1 : output[suit] + 1;
            }
 
            return output;
        }
 
        public void Reset()
        {
            InitializeDeck();
        }
    }
}