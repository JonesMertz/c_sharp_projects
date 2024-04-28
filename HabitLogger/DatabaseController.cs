// use this class to interact with the database
// this class uses sqlite to interact with the database
// it has methods to save a class, it extracts the fields of the class and saves them to the database
// it will create a new database if it does not exist
// it will create a new table for each class if it does not exist
// it has methods to load a class, it will extract the fields of the class from the database and create a new instance of the class

using System;
using Microsoft.Data.Sqlite;


public class DatabaseController
{
    private string connectionString;

    public DatabaseController(string databasePath)
    {
        if (!System.IO.Directory.Exists("databases"))
        {
            System.IO.Directory.CreateDirectory("databases");
        }
        connectionString = $"Data Source=databases/{databasePath}";
        var connection = new SqliteConnection(connectionString);
        connection.Open();
        initiateHabitTable(connection);
        Console.WriteLine($"Initated: {connectionString} database.");
        Console.WriteLine("");
    }

    void initiateHabitTable(SqliteConnection connection)
    {
        var command = connection.CreateCommand();
        command.CommandText =
        @"
                    CREATE TABLE IF NOT EXISTS Habits (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Description TEXT NOT NULL,
                        Amount REAL NOT NULL,
                        Unit TEXT NOT NULL
                    );
                ";
        command.ExecuteNonQuery();
    }

    public Habit CreateHabit(string Name, string Description, double Amount, string Unit)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText =
        @"
            INSERT INTO Habits (Name, Description, Amount, Unit)
            VALUES ($Name, $Description, $Amount, $Unit);
            SELECT last_insert_rowid();
            ";
        command.Parameters.AddWithValue("$Name", Name);
        command.Parameters.AddWithValue("$Description", Description);
        command.Parameters.AddWithValue("$Amount", Amount);
        command.Parameters.AddWithValue("$Unit", Unit);
        var id = command.ExecuteScalar();

        if (id == null)
        {
            throw new Exception("Failed to save habit");
        }

        return new Habit((int)(long)id, Name, Description, Amount, Unit);

    }

    public Habit? LoadHabit(int id)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText =
        @"
            SELECT * FROM Habits WHERE Id = $Id;
            ";
        command.Parameters.AddWithValue("$Id", id);

        using var reader = command.ExecuteReader();
        if (!reader.Read())
        {
            return null;
        }

        return new Habit(
            reader.GetInt32(0),
            reader.GetString(1),
            reader.GetString(2),
            reader.GetDouble(3),
            reader.GetString(4)
            );
    }

    public List<Habit> LoadHabits()
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText =
        @"
            SELECT * FROM Habits;
            ";

        using var reader = command.ExecuteReader();
        var habits = new List<Habit>();
        while (reader.Read())
        {
            habits.Add(new Habit(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetDouble(3),
                reader.GetString(4)
                ));
        }

        return habits;
    }
    public bool DeleteHabit(int id)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText =
        @"
            DELETE FROM Habits WHERE Id = $Id;
            ";
        command.Parameters.AddWithValue("$Id", id);
        command.ExecuteNonQuery();

        return true;
    }

    public bool DeleteHabit(Habit habit)
    {
        if (habit.Id == null)
        {
            throw new ArgumentException("Habit does not have an Id");
        }
        return DeleteHabit((int)habit.Id);
    }

    public bool UpdateHabit(Habit habit)
    {
        using SqliteConnection connection = new SqliteConnection(connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText =
        @"
            UPDATE Habits
            SET Name = $Name, Description = $Description, Amount = $Amount, Unit = $Unit
            WHERE Id = $Id;
            ";
        command.Parameters.AddWithValue("$Name", habit.Name);
        command.Parameters.AddWithValue("$Description", habit.Description);
        command.Parameters.AddWithValue("$Amount", habit.Amount);
        command.Parameters.AddWithValue("$Unit", habit.Unit);
        command.Parameters.AddWithValue("$Id", habit.Id);
        command.ExecuteNonQuery();

        return true;
    }
}
