

class GameController
{
    private readonly Game game;
    private readonly GameView view;

    public GameController(Game game, GameView view)
    {
        this.game = game;
        this.view = view;
    }

    public void StartGame()
    {
        view.Clear();
        game.InitializeGame();

        runGameLoop();
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
        view.DisplayGameEndMessage(game.Score);
    }
}