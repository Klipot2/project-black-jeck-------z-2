public static void DrawCards(List<Card> cards, int cardsPerLine)
{
    int totalCards = cards.Count;
    int totalLines = (int)Math.Ceiling((double)totalCards / cardsPerLine);

    for (int line = 0; line < totalLines; line++)
    {
        DrawTopLine(cards, line, cardsPerLine);
        DrawCardContent(cards, line, cardsPerLine);
        DrawBottomLine(cards, line, cardsPerLine);
    }
}

private static void DrawTopLine(List<Card> cards, int line, int cardsPerLine)
{
    for (int i = 0; i < cardsPerLine; i++)
    {
        int cardIndex = line * cardsPerLine + i;
        if (cardIndex < cards.Count)
        {
            Console.Write(" ____  ");
        }
    }
    Console.WriteLine();
}

private static void DrawCardContent(List<Card> cards, int line, int cardsPerLine)
{
    for (int i = 0; i < cardsPerLine; i++)
    {
        int cardIndex = line * cardsPerLine + i;
        if (cardIndex < cards.Count)
        {
            DrawCard(cards[cardIndex]);
        }
    }
    Console.WriteLine();
}

private static void DrawBottomLine(List<Card> cards, int line, int cardsPerLine)
{
    for (int i = 0; i < cardsPerLine; i++)
    {
        int cardIndex = line * cardsPerLine + i;
        if (cardIndex < cards.Count)
        {
            Console.Write("|    | ");
        }
    }
    Console.WriteLine();
}

public static void DrawCard(Card card)
{
    Console.ForegroundColor = ConsoleColor.White;

    Console.Write("|");

    // If the card value is 10, print "10" instead of "T"
    Console.Write($"{(card.CardValue == Card.Value.Ten ? "10" : card.CardValue.ToString().PadLeft(2))} ");

    // Draw the suit
    DrawCardSuitValue(card);

    Console.Write("| ");
}

private static void DrawCardSuitValue(Card card)
{
    char cardSuit = ' ';
    ConsoleColor fontColor = ConsoleColor.White; // Default color

    switch (card.CardSuit)
    {
        case Card.Suit.H:
            cardSuit = '\u2665';
            fontColor = ConsoleColor.Red;
            break;
        case Card.Suit.D:
            cardSuit = '\u2666';
            fontColor = ConsoleColor.Red;
            break;
        case Card.Suit.C:
            cardSuit = '\u2663';
            fontColor = ConsoleColor.Black;
            break;
        case Card.Suit.S:
            cardSuit = '\u2660';
            fontColor = ConsoleColor.Black;
            break;
    }

    Console.ForegroundColor = fontColor;
    Console.Write($"{cardSuit}");
    Console.ResetColor();
}
