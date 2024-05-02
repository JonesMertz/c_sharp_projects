using System.Reflection.Metadata.Ecma335;

class CodingSession
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }

    private DateTime _endTime { get; set; }
    public DateTime EndTime { get { return IsActive ? DateTime.Now : _endTime; } set { _endTime = value; } }
    public string Duration { get { return (EndTime - StartTime).ToString(@"hh\:mm\:ss"); } set { } }

    public string Name { get; set; }

    public bool IsActive { get; set; }

    public CodingSession()
    {
        Name = "Coding Session";
        IsActive = true;
    }
    public CodingSession(bool isActive)
    {
        Name = "Coding Session";
        IsActive = isActive;
    }

}