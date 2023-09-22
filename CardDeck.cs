using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Poker
{
	class CardDeck
	{
		private enum allSuits
		{
			'S',	// Spades
			'H',	// Hearts	
			'C',	// Clubs
			'D'		// Diamonds
		}

		private enum allRanks
		{
			2,3,4,5,6,7,8,9,'T','J','Q','K','A'
		}

		List<Card> cardDeck = new List<Card>();

		foreach(var suit in allSuits)
			{
				
			}

	}
}