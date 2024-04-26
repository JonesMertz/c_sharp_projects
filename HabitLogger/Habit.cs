public class Habit
{
    public Habit(int Id, string Name, string Description, double Amount, string Unit)
    {
        this.Id = Id;
        this.Name = Name;
        this.Description = Description;
        this.Amount = Amount;
        this.Unit = Unit;
    }
    public string Name { get; }

    public int Id { get; }
    public string Description { get; }
    public double Amount { get; }
    public string Unit { get; }
}