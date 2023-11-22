List<string> allowedParams = new() { "log", "clean" };

if (args.Length == 0
    || (args.Length == 1 && !allowedParams.Contains(args[0]))
    || (args.Length == 2 && args[0] != "watch"))
{
    Console.WriteLine(
        "--- Usage SecurityAudit ---\n" +
        "dotnet run -- watch <path>\tStarts monitoring a specified directory for file changes and saves them into a database\n" +
        "dotnet run -- log\t\tPrints out all tracked changes from the database\n" +
        "dotnet run -- clean\t\tClears all entries from the database");
    return;
}

using FileSystemWatcher watcher = new(args[1]);

// continue