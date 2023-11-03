// using FluentAssertions;
// using Poker.Tests;
// using Xunit;

// namespace Poker.Tests
// {
//     [Fact]
//     public class DeckCardsTests
//     {
//         []
//         public void TestSetUpDeck_FillsDeckCorrectly()
//         {
//             // Arrange
//             DeckCards deck = new DeckCards();

//             // Act
//             deck.setUpDeck();

//             // Assert
//             Assert.AreEqual(52, deck.getDeck.Length); // Проверяем, что массив наполнился 52 картами
//             // Также можно проверить, что все масти и значения присутствуют в колоде
//             // Например, можно проверить, что колода содержит одну карту "2 of Spades" и одну карту "3 of Hearts" и так далее.
//         }

//         []
//         public void TestShuffleCards_ShufflesDeck()
//         {
//             // Arrange
//             DeckCards deck = new DeckCards();

//             // Act
//             deck.setUpDeck(); // Сначала заполним колоду
//             Card[] originalDeck = (Card[])deck.getDeck.Clone(); // Создадим копию "оригинальной" колоды
//             deck.ShuffleCards(); // Перемешаем колоду

//             // Assert
//             CollectionAssert.AreNotEqual(originalDeck, deck.getDeck); // Проверим, что после перемешивания колода не такая же, как оригинальная
//         }
//     }
// }