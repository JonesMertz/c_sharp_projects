abstract class Game
{
    public int Number1;
    public int Number2;
    public int answer;

    public int Lives { get; set; }
    public int Score { get; set; }

    public bool InProgress { get; set; }

    public void InitializeGame()
    {
        Score = 0;
        InProgress = true;
        Lives = 3;
        NextQuestion();
    }
    public abstract void NextQuestion();

    public abstract string GetOperation();
    public bool checkAnswer(int answer)
    {
        return this.answer == answer;
    }
    public void EndGame()
    {
        InProgress = false;
    }
}


class AdditionGame : Game
{
    public override void NextQuestion()
    {
        Random random = new Random();
        Number1 = random.Next(1, 100);
        Number2 = random.Next(1, 100);
        answer = Number1 + Number2;
    }
    public override string GetOperation()
    {
        return "+";
    }
}

class SubtractionGame : Game
{
    public override void NextQuestion()
    {
        Random random = new Random();
        Number1 = random.Next(1, 100);
        Number2 = random.Next(1, 100);
        answer = Number1 - Number2;
    }
    public override string GetOperation()
    {
        return "-";
    }
}

class MultiplicationGame : Game
{
    public override void NextQuestion()
    {
        Random random = new Random();
        Number1 = random.Next(1, 100);
        Number2 = random.Next(1, 100);
        answer = Number1 * Number2;
    }
    public override string GetOperation()
    {
        return "*";
    }
}

class DivisionGame : Game
{
    public override void NextQuestion()
    {
        Random random = new Random();
        while (true)
        {
            int num1 = random.Next(1, 100);
            int num2 = random.Next(1, 100);
            double result = (double)num1 / num2;
            if (num1 == num2)
            {
                continue;
            }

            if (result >= 1 && result <= 100 && Math.Floor(result) == result)
            {
                Number1 = num1;
                Number2 = num2;
                answer = (int)result;
                break;
            }
        }
    }
    public override string GetOperation()
    {
        return "/";
    }
}
