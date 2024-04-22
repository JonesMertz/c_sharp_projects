class GameView
{

    public void DisplayGameStartMessage()
    {
        Console.WriteLine("Game starting...");
    }

    public void DisplayMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void DisplayMenu()
    {
        Console.WriteLine("Welcome to the Math Game!");
        Clear();
        Console.WriteLine("Welcome to the Math Game!");
        Console.WriteLine("_________________________________________________________");
        Console.WriteLine("You can select from the following types of games:");
        Console.WriteLine($"- Addition         A ");
        Console.WriteLine($"- Subtraction      S ");
        Console.WriteLine($"- Multiplication   M ");
        Console.WriteLine($"- Division         D ");
        Console.WriteLine($"- Game history     H ");
        Console.WriteLine($"- Exit             E ");
        Console.WriteLine("_________________________________________________________");
        Console.WriteLine("Type the letter of the game you want to play and press Enter.");
    }

    public void DisplayScore(int score)
    {
        Console.WriteLine($"Score: {score}");
    }

    public void DisplayLives(int lives)
    {
        Console.WriteLine($"Lives: {lives}");
    }

    public void DisplayQuestion(int number1, int number2, string operation)
    {
        Console.WriteLine($"What is {number1} {operation} {number2}?");
    }

    public void DisplayQuestionView(int number1, int number2, string operation, int score, int lives)
    {
        DisplayScore(score);
        DisplayLives(lives);
        DisplayQuestion(number1, number2, operation);
    }

    public void DisplayGameEndMessage(int score)
    {
        Clear();
        Console.WriteLine("Game over!");
        Console.WriteLine($"Your score was: {score}");
        Console.WriteLine(" ");
        Console.WriteLine("Press any key to return to the main menu.");
        Console.ReadKey();
    }

    public void Clear()
    {
        try { Console.Clear(); }
        catch { }
    }
}