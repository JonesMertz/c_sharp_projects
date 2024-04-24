

class GameController
{
    private Game game;
    private readonly GameView view;

    List<string> gameHistory = new List<string>();

    public GameController(GameView view)
    {
        game = new AdditionGame();
        this.view = view;
    }

    public void StartGame()
    {
        while (true)
        {
            view.Clear();
            runMenuLoop();

            game.InitializeGame();

            runGameLoop();
        }
    }

    public void runMenuLoop()
    {
        while (true)
        {
            view.DisplayMenu();
            string input = Console.ReadLine() ?? "";

            input = input.ToLower();
            switch (input)
            {
                case "a":
                    game = new AdditionGame();
                    break;
                case "s":
                    game = new SubtractionGame();
                    break;
                case "m":
                    game = new MultiplicationGame();
                    break;
                case "d":
                    game = new DivisionGame();
                    break;
                case "h":
                    view.DisplayGameHistory(gameHistory);
                    continue;
                case "e":
                    view.Clear();
                    view.DisplayMessage("Thanks for playing!");
                    Environment.Exit(0);
                    break;
                default:
                    continue;
            }
            break;
        }
    }

    public void runGameLoop()
    {
        bool errorOccured = false;
        while (game.InProgress)
        {
            if (!errorOccured)
            {
                game.NextQuestion();
            }
            errorOccured = false;
            view.Clear();
            view.DisplayQuestionView(game.Number1, game.Number2, game.GetOperation(), game.Score, game.Lives);

            string input = Console.ReadLine() ?? "";

            if (input == "exit")
            {
                EndGame();
            }

            if (int.TryParse(input, out int answer))
            {
                handleAnswer(answer);
            }
            else
            {
                view.DisplayMessage("Invalid input. Please enter a number...");
                errorOccured = true;
                Console.ReadKey();
            }

            if (game.Lives == 0)
            {
                EndGame();
                break;
            }
        }
    }

    public void handleAnswer(int answer)
    {
        if (game.checkAnswer(answer))
        {
            view.DisplayMessage("Correct!");
            view.DisplayMessage("Press any key to continue...");
            Console.ReadKey();
            game.Score++;
        }
        else
        {
            view.DisplayMessage("Incorrect!");
            view.DisplayMessage("The correct answer was " + game.answer);
            view.DisplayMessage("Press any key to continue...");
            Console.ReadKey();
            game.Lives--;
        }
    }

    public void EndGame()
    {
        game.EndGame();
        AddGameToHistory(game);
        view.DisplayGameEndMessage(game.Score);
    }

    public void AddGameToHistory(Game game)
    {

        gameHistory.Add($"{DateTime.Now} - {game.Type} - Score: {game.Score}");
    }
}