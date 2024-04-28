using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data.Common;
using System.Runtime;

class HabitController
{
    private DatabaseController _databaseController;
    private View _view;
    public HabitController(DatabaseController databaseController)
    {
        _databaseController = databaseController;
        _view = new View();
        databaseController.LoadHabits().ForEach(habit => _view.Habit(habit));

    }

    public void GetUserInput()
    {
        while (true)
        {
            _view.Clear();
            _view.Menu();
            string input = Console.ReadLine() ?? "";
            switch (input)
            {
                case "1":
                    _view.Clear();
                    _view.Habits(_databaseController.LoadHabits());
                    Console.ReadKey();
                    break;
                case "2":
                    _view.Clear();
                    try { CreateHabit(); }
                    catch (Exception e)
                    {
                        _view.Message(e.Message);
                    }
                    Console.ReadKey();
                    break;
                case "3":
                    _view.Clear();
                    DeleteHabit();
                    Console.ReadKey();
                    break;
                case "4":
                    _view.Clear();
                    UpdateHabit();
                    Console.ReadKey();
                    break;
                case "0":
                    _view.Clear();
                    _view.Message("Goodbye!");
                    Environment.Exit(0);
                    return;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
        }
    }

    Habit CreateHabit()
    {
        (string name, string description, double amount, string unit) = _view.HabitCreation();

        if (name == "" || unit == "")
        {
            throw new Exception("Invalid input");
        }
        Habit habit = _databaseController.CreateHabit(name, description, amount, unit);
        _view.Message("Habit created");
        return habit;
    }

    Habit GetHabit(int id)
    {
        Habit habit = _databaseController.LoadHabit(id) ?? throw new Exception("Habit not found");
        return habit;
    }

    List<Habit> GetHabits()
    {
        return _databaseController.LoadHabits();
    }
    void UpdateHabit()
    {

        int id = _view.SelectHabit(GetHabits());
        Habit selectedHabit = GetHabit(id);
        selectedHabit = _view.HabitUpdate(selectedHabit);

        bool dbSuccess = _databaseController.UpdateHabit(selectedHabit);

        if (dbSuccess)
        {
            _view.Message("Habit updated");
        }
        else
        {
            _view.Message("Habit not found");
        }
    }

    void DeleteHabit()
    {
        _view.Message("Select habit to delete");
        int id = _view.SelectHabit(GetHabits());
        bool dbSuccess = _databaseController.DeleteHabit(id);
        if (dbSuccess)
        {
            _view.Message("Habit deleted");
        }
        else
        {
            _view.Message("Habit not found");
        }
    }
}