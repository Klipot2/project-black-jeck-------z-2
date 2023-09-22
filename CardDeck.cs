using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Poker
{
	class CardDeck
	{	
		public enum allSuits
		{
			S,	// Spades
			H,	// Hearts	
			C,	// Clubs
			D	// Diamonds
		}

		public enum allRanks
		{
			two = 2,
			three,
			four,
			five,
			six,
			seven,
			eight,
			nine,
			ten,
			J, //Jack
			Q,	//Queen
			K,	//King
			A	// Ace
		}

		List<Card> cardDeck = new List<Card>();

		
		private void fillDeck(){
			foreach(var suit in allSuits)
			{
				foreach(var rank in allRanks)
				{
					Card temporaryCard = new Card(rank,suit);
					cardDeck.Add(temporaryCard);
				}
			}
		}

		public CardDeck(){
			fillDeck()
		}	
		public void writeDeck(){
			Console.WriteLine(cardDeck);
		}
	}
}