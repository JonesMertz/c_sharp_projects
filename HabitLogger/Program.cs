// See https://aka.ms/new-console-template for more information

DatabaseController databaseController = new DatabaseController("habits.db");
HabitController habitController = new HabitController(databaseController);
View view = new View();

databaseController.LoadHabits().ForEach(habit => view.Habit(habit));

while (true)
{
    view.Clear();
    view.Menu();
    string input = Console.ReadLine();

    switch (input)
    {
        case "1":
            databaseController.LoadHabits().ForEach(habit => view.Habit(habit));
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            break;
        case "2":
            object habit = view.HabitCreation();
            //habitController.CreateHabit(habit, habit.Description, habit.Amount, habit.Unit);
            break;
        case "3":
            Console.WriteLine("Enter habit name:");
            string habitName = Console.ReadLine() ?? "";
            Habit habitToDelete = habitController.GetHabits().Find(habit => habit.Name == habitName);
            if (habitToDelete == null)
            {
                view.Message("Habit not found");
                Console.ReadKey();
                break;
            }
            int id = habitToDelete.Id;
            habitController.DeleteHabit(id);
            break;
        case "E":
            Environment.Exit(0);
            return;
        default:
            Console.WriteLine("Invalid input");
            break;
    }
}



//habitController.CreateHabit(new Habit("Drink water", "Drink 2 liters of water", 2, "liters"));

//databaseController.SaveHabit(new Habit("Drink water", "Drink 2 liters of water", 2, "liters"));
//databaseController.DeleteHabit(1);
/* HabitController habitController = new HabitController();
View view = new View();

while (true)
{
    Console.WriteLine("1. Create habit");
    string input = Console.ReadLine();

    switch (input)
    {
        case "1":
            Habit habit = view.HabitCreation();
            habitController.CreateHabit(habit);
            break;
        case "2":
            Console.WriteLine("Enter habit id:");
            int id = int.Parse(Console.ReadLine());
            habit = habitController.getHabit(id);
            Console.WriteLine(habit);
            break;
        case "3":
            return;
        default:
            Console.WriteLine("Invalid input");
            break;
    }
} */