// using FluentAssertions;
// using Poker;
// using Xunit;

// public class DrawCardsTests
// {
//     [Fact]
//     public void DrawCardOutline_ShouldDrawCardOutlineCorrectly()
//     {
//         // Arrange
//         var expectedOutput = new StringWriter();
//         Console.SetOut(expectedOutput);

//         // Act
//         DrawCards.DrawCardOutline(0, 0);

//         // Assert
//         var actualOutput = expectedOutput.ToString();
//         var expectedCardOutline = " ________\n|        |\n|        |\n|        |\n|        |\n|        |\n|        |\n|        |\n|        |\n|        |\n|________|\n";
//         actualOutput.Should().Be(expectedCardOutline);
//     }
//     [Fact]
//     public void DrawCardSuitValue_ShouldDrawCardSuitValueCorrectly()
//     {
//         // Arrange
//         var expectedOutput = new StringWriter();
//         Card card = new(Card.Suit.H, Card.Value.Ace);
//         //TODO: add card line constructor
//         var expectedCard = " ________\n| \u2665      |\n|   A     |\n|        |\n|        |\n|        |\n|        |\n|        |\n|        |\n|        |\n|________|\n";

//         // Act
//         Console.SetOut(expectedOutput);
//         var actualOutput = expectedOutput.ToString();
//         DrawCards.DrawCardSuitValue(card, 0, 0);

//         // Assert
//         actualOutput.Should().Be(expectedCard);
//     }
// }