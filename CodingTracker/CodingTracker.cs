using Spectre.Console;
using System.Globalization;

class CodingTracker
{
    private const string START_CODING_SESSION = "Start Coding Session";
    private const string END_CODING = "End Coding Session";
    private const string LOG_CODING_SESSION = "Log Coding Session";
    private const string VIEW_CODING_SESSIONS = "View Coding Sessions";

    private const string REPORT_CODING_SESSION = "Reports";
    private const string DELETE_CODING_SESSION = "Delete Coding Session";
    private const string EXIT = "Exit";
    private DatabaseService _databaseService;
    public CodingTracker(DatabaseService databaseService)
    {
        _databaseService = databaseService;

    }

    public void startTracker()
    {
        while (true)
        {
            Menu();
        }
    }

    private void Menu()
    {
        AnsiConsole.Clear();

        List<string> menuOptions = new List<string>
        {
            START_CODING_SESSION,
            END_CODING,
            LOG_CODING_SESSION,
            VIEW_CODING_SESSIONS,
            DELETE_CODING_SESSION,
            EXIT,
        };
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Welcome to the Coding Tracker")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(menuOptions)
        );
        try
        {
            HandleSelection(selection);
        }
        catch (Exception e)
        {
            AnsiConsole.MarkupLine($"[red]Error: {e.Message}[/]");
            Console.ReadKey();
        }
    }

    private void HandleSelection(string selection)
    {
        switch (selection)
        {
            case START_CODING_SESSION:
                StartCodingSession();
                break;
            case END_CODING:
                EndCodingSession();
                break;
            case LOG_CODING_SESSION:
                LogCodingSession();
                break;
            case VIEW_CODING_SESSIONS:
                ViewCodingSessions();
                break;
            case DELETE_CODING_SESSION:
                DeleteCodingSession();
                break;
            case REPORT_CODING_SESSION:

                break;
            case EXIT:
                Environment.Exit(0);
                break;
        }
    }

    private void StartCodingSession()
    {
        if (_databaseService.ActiveCodingSession() != null)
        {
            AnsiConsole.MarkupLine("[red]Error: There is already an active coding session.[/]");
            Console.ReadKey();
            return;
        }

        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("Starting coding session...");
        CodingSession codingSession = new CodingSession
        {
            StartTime = DateTime.Now,
        };
        _databaseService.CreateCodingSession(codingSession);
        AnsiConsole.MarkupLine("[green]Coding session started![/]");
        AnsiConsole.MarkupLine($"Start time: [yellow]{codingSession.StartTime}[/]");
        AnsiConsole.MarkupLine("Press any key to continue...");
        Console.ReadKey();
    }

    private void EndCodingSession()
    {
        CodingSession? codingSession = _databaseService.ActiveCodingSession();
        if (codingSession == null)
        {
            AnsiConsole.MarkupLine("[red]Error: There is no active coding session to end.[/]");
            Console.ReadKey();
            return;
        }

        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("Ending coding session...");
        codingSession.EndTime = DateTime.Now;
        codingSession.IsActive = false;
        _databaseService.UpdateCodingSession(codingSession);

        AnsiConsole.MarkupLine("[green]Coding session ended![/]");
        AnsiConsole.MarkupLine($"End time: [yellow]{codingSession.EndTime}[/]");
        AnsiConsole.MarkupLine($"Duration: [yellow]{codingSession.Duration}[/]");
        Console.ReadKey();
    }

    private void LogCodingSession()
    {
        // i need an optional name, a start time and end time
        AnsiConsole.Clear();

        string name = AnsiConsole.Ask<string>("Enter the name of the coding session:");
        string startTimeString = AnsiConsole.Prompt(new TextPrompt<string>("Enter the start time of the coding session:")
            .ValidationErrorMessage("[red]Invalid date format. Please enter a valid date and time in the format 'yyyy-MM-dd HH:mm'[/]").Validate((string input) =>
            {
                try
                {
                    DateTime.ParseExact(input, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                    return true;
                }
                catch
                {
                    return false;
                }
            })
            );
        DateTime startTime = DateTime.ParseExact(startTimeString, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);


        string endTimeString = AnsiConsole.Prompt(new TextPrompt<string>("Enter the start time of the coding session:")
            .ValidationErrorMessage("[red]Invalid date format. Please enter a valid date and time in the format 'yyyy-MM-dd HH:mm'[/]").Validate((string input) =>
            {
                try
                {
                    DateTime.ParseExact(input, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                    return true;
                }
                catch
                {
                    return false;
                }
            })
            );
        DateTime endTime = DateTime.ParseExact(endTimeString, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

        CodingSession codingSession = new CodingSession
        {
            Name = name,
            StartTime = startTime,
            EndTime = endTime,
            IsActive = false,
        };

        _databaseService.CreateCodingSession(codingSession);
        AnsiConsole.MarkupLine("[green]Coding session logged![/]");
    }

    private void ViewCodingSessions()
    {
        List<CodingSession> codingSessions = _databaseService.GetCodingSessions();
        AnsiConsole.Clear();




        DisplayCodingSessions(codingSessions);
        AnsiConsole.MarkupLine("[yellow]Press any key to continue...[/]");
        Console.ReadKey();
    }

    private void DeleteCodingSession()
    {
        AnsiConsole.Clear();
        List<CodingSession> codingSessions = _databaseService.GetCodingSessions();
        DisplayCodingSessions(codingSessions);
        int id = AnsiConsole.Ask<int>("Enter the ID of the coding session to delete:");
        _databaseService.DeleteCodingSession(id);

        AnsiConsole.MarkupLine("[green]Coding session deleted![/]");
        Console.ReadKey();
    }

    private void DisplayCodingSessions(List<CodingSession> codingSessions)
    {
        Table codingSessionsTable = new Table();
        codingSessionsTable.Title(new TableTitle("Coding Sessions"));
        codingSessionsTable.AddColumns("Id", "Name", "Start Time", "End Time", "Duration");
        codingSessionsTable.Columns[0].Width = 5;

        foreach (CodingSession codingSession in codingSessions)
        {
            Style? activeStyle = codingSession.IsActive ? Style.Parse("green") : null;
            Markup[] columns =
            [
                new Markup(codingSession.Id.ToString(), activeStyle),
                new Markup(codingSession.Name, activeStyle),
                new Markup(codingSession.StartTime.ToString(), activeStyle),
                new Markup(codingSession.EndTime.ToString(), activeStyle),
                new Markup(codingSession.Duration, activeStyle),
            ];
            codingSessionsTable.AddRow(columns);
        }
        AnsiConsole.Write(codingSessionsTable);
        AnsiConsole.MarkupLine("[green]Green rows indicate the active coding session.[/]");

    }

    private void DisplayReportMenus()
    {
        List<string> menuOptions = new List<string>
        {
            "Daily Report",
            "Weekly Report",
            "Monthly Report",
            "Yearly Report",
            "Back",
        };
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a report to view")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(menuOptions)
        );
        try
        {
            HandleReportSelection(selection);
        }
        catch (Exception e)
        {
            AnsiConsole.MarkupLine($"[red]Error: {e.Message}[/]");
            Console.ReadKey();
        }
    }


}