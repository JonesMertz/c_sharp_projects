// This is the main program file for the CodingTracker application. It contains the main method that is called when the application is run.
using Microsoft.Extensions.Configuration;
IConfigurationRoot config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

string connectionString = config.GetValue<string>("ConnectionString") ?? "";

if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("Connection string is missing from appsettings.json or environment variables.");
    return;
}

DatabaseService databaseService = new DatabaseService(connectionString);
CodingTracker codingTracker = new CodingTracker(databaseService);
try
{
    codingTracker.startTracker();

}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}