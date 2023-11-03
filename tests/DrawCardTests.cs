using FluentAssertions;
using Poker;
using System;
using System.IO;
using Xunit;

public class DrawCardsTests
{
    [Fact]
    public void DrawCardOutline_ShouldDrawCardOutlineCorrectly()
    {
        // Arrange
        var expectedOutput = new StringWriter();
        Console.SetOut(expectedOutput);

        // Act
        DrawCards.DrawCardOutline(0, 0);

        // Assert
        var actualOutput = expectedOutput.ToString();
        var expectedCardOutline = " ________\n|        |\n|        |\n|        |\n|        |\n|        |\n|        |\n|        |\n|        |\n|        |\n|________|\n";
        actualOutput.Should().Be(expectedCardOutline);
    }
    [Fact]
    public void DrawCardSuitValue_ShouldDrawCardSuitValueCorrectly()
    {
        // Arrange
        var expectedOutput = new StringWriter();
        Console.SetOut(expectedOutput);
        Card card = new Card { MySuit = Card.SUIT.H, MyValue = Card.VALUE.Ace };

        // Act
        DrawCards.DrawCardSuitValue(card, 0, 0);

        // Assert
        var actualOutput = expectedOutput.ToString();
        var expectedCard = " ________\n| \u2665      |\n|   A     |\n|        |\n|        |\n|        |\n|        |\n|        |\n|        |\n|        |\n|________|\n";
        actualOutput.Should().Be(expectedCard);
    }
}