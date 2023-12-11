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
    Console.Write("|");

    string valueString = card.CardValue switch
    {
        Card.Value.Ten => "10",
        _ => card.CardValue.ToString().Substring(0, 1)
    };

    Console.Write($"{valueString.PadRight(4)}");

    // Draw the suit
    DrawCardSuitValue(card);

    Console.Write("| ");
}

private static void DrawCardSuitValue(Card card)
{
    char cardSuit = ' ';

    switch (card.CardSuit)
    {
        case Card.Suit.H:
            cardSuit = '\u2665';
            break;
        case Card.Suit.D:
            cardSuit = '\u2666';
            break;
        case Card.Suit.C:
            cardSuit = '\u2663';
            break;
        case Card.Suit.S:
            cardSuit = '\u2660';
            break;
    }

    Console.Write($"{cardSuit}  ");
}
