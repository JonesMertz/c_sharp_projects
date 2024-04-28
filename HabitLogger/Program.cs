// See https://aka.ms/new-console-template for more information

DatabaseController databaseController = new DatabaseController("habits.db");
HabitController habitController = new HabitController(databaseController);
View view = new View();

habitController.GetUserInput();




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