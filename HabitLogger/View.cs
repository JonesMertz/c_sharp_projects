class View
{
    public void Clear()
    {
        Console.Clear();
    }
    public void Habits(List<Habit> habits)
    {
        if (habits.Count == 0)
        {
            Console.WriteLine("No habits found");
            return;
        }

        Console.WriteLine("Habits");
        Console.WriteLine("------");
        foreach (Habit habit in habits)
        {
            Habit(habit);
        }
    }

    public void Habit(Habit habit)
    {
        Console.WriteLine();
        Console.WriteLine($"Name: {habit.Name}");
        Console.WriteLine($"Current count: {habit.Amount} {habit.Unit}");
        Console.WriteLine($"Description: {habit.Description}");
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
        Console.WriteLine("Exit".PadRight(padding) + "0");
    }
    public (string, string, double, string) HabitCreation()
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
        return (name, description, amount, unit);
    }
    public int HabitDeletion(List<Habit> habits)
    {
        Console.WriteLine("Habits:");
        foreach (Habit habit in habits)
        {
            Console.WriteLine($"{habit.Id}: {habit.Name}");
        }
        Console.WriteLine();
        Console.Write("Enter habit id to delete:");

        while (true)
        {
            string input = Console.ReadLine() ?? "";
            if (checkInput(input, "int"))
            {
                return int.Parse(input);
            }
            Error("Invalid input");
        }
    }
    public Habit HabitUpdate(Habit habit)
    {
        while (true)
        {
            Clear();
            Console.WriteLine($"{habit.Name} selected");
            Console.WriteLine("Select a property to update:");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Description");
            Console.WriteLine("3. Amount");
            Console.WriteLine("4. Unit");
            Console.WriteLine("0. Save");

            Console.Write("Enter number to select property: ");
            string input = Console.ReadLine() ?? "";

            switch (input)
            {
                case "1":
                    Console.Write("Enter new name: ");
                    string name = Console.ReadLine() ?? "";

                    if (checkInput(name, "string"))
                    {
                        habit.Name = name;
                    }
                    else
                    {
                        Error("Name cannot be empty");
                    }
                    break;
                case "2":
                    Console.Write("Enter new description:");
                    string description = Console.ReadLine() ?? "";
                    habit.Description = description;
                    break;
                case "3":
                    Console.Write("Enter new amount: ");
                    string amount = Console.ReadLine() ?? "";

                    if (checkInput(amount, "double"))
                    {
                        habit.Amount = double.Parse(amount);
                    }
                    else
                    {
                        Error("Amount needs to be a number");
                    }
                    break;
                case "4":
                    Console.Write("Enter new unit: ");
                    string unit = Console.ReadLine() ?? "";

                    if (checkInput(unit, "string"))
                    {
                        habit.Unit = unit;
                    }
                    else
                    {
                        Error("Unit cannot be empty");
                    }
                    break;
                case "0":
                    return habit;
                default:
                    Error("Invalid input");
                    break;
            }
        }
    }

    public int SelectHabit(List<Habit> habits)
    {
        Console.WriteLine("Habits:");
        foreach (Habit habit in habits)
        {
            Console.WriteLine($"{habit.Id}: {habit.Name}");
        }

        Console.Write("Enter habit id: ");
        while (true)
        {
            string input = Console.ReadLine() ?? "";
            if (checkInput(input, "int"))
            {
                return int.Parse(input);
            }
            Error("Invalid input");
        }
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

