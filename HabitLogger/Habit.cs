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
    public int Id { get; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Amount { get; set; }
    public string Unit { get; set; }
}