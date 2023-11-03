using FluentAssertions;
using Poker;
using System;
using System.IO;
using Xunit;

public class PokerGameTests
{
    [Fact]
    public void PlayGame_ShouldHandleUserInputAndQuitCorrectly()
    {
        // Arrange
        PokerGame pokerGame = new PokerGame();
        var originalInput = Console.In;
        var originalOutput = Console.Out;

        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw);

            // Set the input to simulate user input
            using (StringReader sr = new StringReader("100\nN\n"))
            {
                Console.SetIn(sr);

                // Act
                pokerGame.PlayGame();

                // Assert
                var output = sw.ToString();
                output.Should().Contain("Enter your bet amount:");
                output.Should().Contain("Play again? Y-N");
                output.Should().Contain("Your current bet amount: 0");
            }
        }

        // Restore the original Console.In and Console.Out
        Console.SetIn(originalInput);
        Console.SetOut(originalOutput);
    }
}