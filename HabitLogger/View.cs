class View
{
    public void Clear()
    {
        Console.Clear();
    }
    public void Habits(Habit[] habits)
    {
        Console.WriteLine("Habits:");
        foreach (Habit habit in habits)
        {
            Habit(habit);
        }
    }

    public void Habit(Habit habit)
    {
        Console.WriteLine("Habit:");
        Console.WriteLine($"Name: {habit.Name}");
        Console.WriteLine($"Description: {habit.Description}");
        Console.WriteLine($"{habit.Amount} {habit.Unit}");
        Console.WriteLine();
    }

    public void Menu()
    {
        var padding = 30;
        Console.WriteLine("Welcome to your person habit logger!");
        Console.WriteLine("------------------------------------");
        Console.WriteLine("Menu:");
        Console.WriteLine("View habits".PadRight(padding) + "1");
        Console.WriteLine("Create habit".PadRight(padding) + "2");
        Console.WriteLine("Delete habit".PadRight(padding) + "3");
        Console.WriteLine("Update habit".PadRight(padding) + "4");
        Console.WriteLine("Exit".PadRight(padding) + "E");
    }
    public object HabitCreation()
    {
        string name;
        string description;
        double amount;
        string unit;

        string input;
        while (true)
        {
            Console.Write("Enter habit name: ");
            input = Console.ReadLine() ?? "";
            if (checkInput(input, "string"))
            {
                name = input;
                break;
            }
            Error("Name cannot be empty");
        }
        Console.Write("Enter habit name: ");
        Console.Write("Enter habit description: ");

        while (true)
        {
            Console.Write("Enter habit description: ");
            input = Console.ReadLine() ?? "";
            if (checkInput(input, "string"))
            {
                description = input;
                break;
            }
            Error("Description cannot be empty");
        }

        while (true)
        {
            Console.Write("Enter habit amount: ");
            input = Console.ReadLine() ?? "";
            if (checkInput(input, "double"))
            {
                double.TryParse(input, out amount);
                break;
            }
            Error("Amount must be a number");
        }

        while (true)
        {
            Console.Write("Enter habit unit: ");
            input = Console.ReadLine() ?? "";
            if (checkInput(input, "string"))
            {
                unit = input;
                break;
            }
            Error("Unit cannot be empty");
        }
        // return simple object with the data
        return new { Name = name, Description = description, Amount = amount, Unit = unit };
    }
    public void HabitDeletion()
    {
    }
    public void HabitUpdate()
    {
    }

    public void Error(string message)
    {
        Console.WriteLine($"Error: {message}");
    }

    public void Message(string message)
    {
        Console.WriteLine(message);
    }

    public bool checkInput(string input, string type)
    {
        switch (type)
        {
            case "string":
                if (input == "")
                {
                    return false;
                }
                return true;
            case "int":
                return int.TryParse(input, out _);
            case "double":
                return double.TryParse(input, out _);
            default:
                return true;
        }
    }
}

