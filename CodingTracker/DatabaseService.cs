using System.Data;
using System.Data.SQLite;
using Dapper;

class DatabaseService
{
    private string _connectionString = "";
    public DatabaseService(string connectionString)
    {
        _connectionString = connectionString;
        SqlMapper.AddTypeHandler(new TimeSpanHandler());

        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Execute("CREATE TABLE IF NOT EXISTS CodingSessions (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, StartTime TEXT, EndTime TEXT, Duration INT, IsActive INTEGER DEFAULT 1)");
        }
    }

    public void CreateCodingSession(CodingSession session)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            string sql = "INSERT INTO CodingSessions (StartTime, EndTime, Duration, Name, IsActive) VALUES (@StartTime, @EndTime, @Duration, @Name, @IsActive)";
            connection.Execute(sql, session);
        }
    }

    public void UpdateCodingSession(CodingSession session)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            string sql = "UPDATE CodingSessions SET StartTime = @StartTime, EndTime = @EndTime, Duration = @Duration, IsActive = @IsActive WHERE Id = @Id";
            connection.Execute(sql, session);
        }
    }

    public void DeleteCodingSession(int id)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            string sql = "DELETE FROM CodingSessions WHERE Id = @Id";
            connection.Execute(sql, new { Id = id });
        }
    }

    public CodingSession? ActiveCodingSession()
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            string sql = "SELECT * FROM CodingSessions WHERE IsActive = 1 LIMIT 1";
            return connection.QueryFirstOrDefault<CodingSession>(sql);
        }
    }

    public CodingSession GetCodingSession(int id)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            string sql = "SELECT * FROM CodingSessions WHERE Id = @Id";
            CodingSession codingSession = connection.QueryFirstOrDefault<CodingSession>(sql, new { Id = id });
            if (codingSession == null)
            {
                throw new Exception("Coding session not found");
            }
            return codingSession;
        }
    }

    public List<CodingSession> GetCodingSessions()
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            string sql = "SELECT * FROM CodingSessions ORDER BY Id DESC";
            return connection.Query<CodingSession>(sql).ToList();
        }
    }

}

class TimeSpanHandler : SqlMapper.TypeHandler<TimeSpan>
{
    public override TimeSpan Parse(object value)
    {
        Console.WriteLine(value);
        return TimeSpan.FromTicks((int)value);
    }

    public override void SetValue(IDbDataParameter parameter, TimeSpan value)
    {
        parameter.Value = value.Ticks;
    }
}