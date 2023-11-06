using FluentAssertions;
using Poker;
using Xunit;

namespace Poker.Tests
{
    public class DeckCardsTests
    {
        [Fact]
        public void TestSetUpDeck_FillsDeckCorrectly()
        {
            // Arrange
            DeckCards deck = new DeckCards();

            // Act
            deck.SetUpDeck();

            // Assert
            deck.GetAllCards.Length.Should().Be(52); // Проверяем, что массив наполнился 52 картами
            // Проверяем, что общее число комбинаций равно 52
            // Для проверки наличия конкретных карт в колоде, можно использовать Assert.Contains:
            // deck.getDeck.Should().Contain(card => card.MyValue == Card.VALUE.Two && card.MySuit == Card.SUIT.S);
            // deck.getDeck.Should().Contain(card => card.MyValue == Card.VALUE.Three && card.MySuit == Card.SUIT.H);
            // И так далее.
        }

        [Fact]
        public void TestShuffleCards_ShufflesDeck()
        {
            // Arrange
            DeckCards deck = new DeckCards();

            // Act
            deck.SetUpDeck(); // Сначала заполним колоду
            Card[] originalDeck = (Card[])deck.GetAllCards.Clone(); // Создадим копию "оригинальной" колоды
            deck.ShuffleCards(); // Перемешаем колоду

            // Assert
            originalDeck.Should().NotEqual(deck.GetAllCards); // Проверим, что после перемешивания колода не такая же, как оригинальная
        }
    }
}