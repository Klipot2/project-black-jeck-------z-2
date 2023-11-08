using FluentAssertions;
using Xunit;
using Casino.CardGames;

namespace Casino.Tests
{
    public class DeckCardsTests
    {
        [Fact]
        public void DeckCards_Constructor()
        {
            // Arrange
            DeckCards deck = new();

            // Act
            int deckSize = deck.DeckSize;

            // Assert
            deckSize.Should().Be(0);
        }
    }
}