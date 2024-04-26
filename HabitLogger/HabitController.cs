using System.ComponentModel.Design;

class HabitController
{
    private DatabaseController _databaseController;
    public HabitController(DatabaseController databaseController)
    {
        _databaseController = databaseController;
    }

    public Habit CreateHabit(string Name, string Description, double Amount, string Unit)
    {
        return _databaseController.CreateHabit(Name, Description, Amount, Unit);
    }

    public Habit GetHabit(int id)
    {
        Habit habit = _databaseController.LoadHabit(id) ?? throw new Exception("Habit not found");
        return habit;
    }

    public List<Habit> GetHabits()
    {
        return _databaseController.LoadHabits();
    }

    public bool DeleteHabit(int id)
    {
        return _databaseController.DeleteHabit(id);
    }

    internal void CreateHabit(object name, object description, object amount, object unit)
    {
        throw new NotImplementedException();
    }
}