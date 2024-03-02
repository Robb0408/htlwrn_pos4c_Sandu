string command = args.Length > 0 ? args[0] : "";
SecurityAudit.Logic.SecurityAudit audit = new(); // Rider does not allow me to type 'using' above?!

switch (command)
{
    case "watch" when args.Length == 2:
        try
        {
            await audit.StartWatcherAsync(args[1]);
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("Directory does not exist");
        }
        break;

    case "log":
        await audit.GetAllEntriesAsync();
        break;

    case "clean":
        await audit.DeleteAllEntriesAsync();
        break;
    default:
        Console.WriteLine(
            "--- Usage SecurityAudit ---\n" +
            "dotnet run -- watch <path>\tStarts monitoring a specified directory for file changes and saves them into a database\n" +
            "dotnet run -- log\t\tPrints out all tracked changes from the database\n" +
            "dotnet run -- clean\t\tClears all entries from the database");
        break;
}