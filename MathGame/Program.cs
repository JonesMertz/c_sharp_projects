GameView view = new GameView();
Game game;
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
        default:
            continue;
    }

    GameController controller = new GameController(game, view);

    controller.StartGame();
}