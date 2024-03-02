using Microsoft.EntityFrameworkCore;

namespace FileAudit.Logic;

public class SecurityWatcher
{
    private FileAuditContext Context { get; } = new();

    /// <summary>
    /// Starts a watcher which watches for changes, creations, deletions and renames.
    /// The captured events will be stored in a database.
    /// </summary>
    /// <param name="path">Path where the watcher acts.</param>
    /// <exception cref="DirectoryNotFoundException"></exception>
    public async Task StartWatcher(string path)
    {

        if (!Directory.Exists(path))
        {
            throw new DirectoryNotFoundException();
        }

        FileSystemWatcher watcher = new()
        {
            Path = path
        };

        watcher.Changed += (sender, eventArgs) =>
        {
            AddFileToContext(Path.GetFileName(eventArgs.FullPath), eventArgs.ChangeType.ToString(), 
                eventArgs.FullPath);
        };
        watcher.Created += (sender, eventArgs) =>
        {
            AddFileToContext(Path.GetFileName(eventArgs.FullPath), eventArgs.ChangeType.ToString(), 
                eventArgs.FullPath);
        };
        watcher.Deleted += (sender, eventArgs) =>
        {
            AddFileToContext(Path.GetFileName(eventArgs.FullPath), eventArgs.ChangeType.ToString(), 
                eventArgs.FullPath);
        };
        watcher.Renamed += (sender, eventArgs) =>
        {
            AddFileToContext($"{Path.GetFileName(eventArgs.OldFullPath)} => " +
                             $"{Path.GetFileName(eventArgs.FullPath)}",
                eventArgs.ChangeType.ToString(),
                eventArgs.FullPath
            );
        };
        watcher.EnableRaisingEvents = true;
        Console.WriteLine(
            "\\ \\        / /  | |     | |              \n" +
            " \\ \\  /\\  / /_ _| |_ ___| |__   ___ _ __ \n" +
            "  \\ \\/  \\/ / _` | __/ __| '_ \\ / _ \\ '__|\n" +
            "   \\  /\\  / (_| | || (__| | | |  __/ |   \n" +
            "    \\/  \\/ \\__,_|\\__\\___|_| |_|\\___|_|  \n" +
            $"\nWatcher started at {watcher.Path}\n" +
            "\nStop watcher by pressing any key . . .");
        Console.ReadKey(true);
        await Context.SaveChangesAsync();
        Console.WriteLine("Watcher stopped!\n" +
                          "View the changes with the <log> command or clear all entries with <clean>");
        watcher.Dispose();
    }

    /// <summary>
    /// Gets all entries from the database.
    /// </summary>
    /// <returns>
    /// List of FileAudit entries.
    /// </returns>
    public async Task GetAllEntries()
    {
        var audits = await Context.FileAudits.ToListAsync();
        if (audits.Count > 0)
        {
            audits.ForEach(audit =>
                Console.WriteLine($"[{audit.Timestamp}] [{audit.ChangeType}] {audit.FileName}"));
        }
        else
        {
            Console.WriteLine("No entries found");   
        }
    }

    /// <summary>
    /// Deletes all entries from the database
    /// </summary>
    public async Task DeleteAllEntries()
    {
        Context.FileAudits.RemoveRange(Context.FileAudits);
        await Context.SaveChangesAsync();
        Console.WriteLine("All entries deleted");
    }

    /// <summary>
    /// Adds an entry to the database
    /// </summary>
    /// <param name="fileName">File name</param>
    /// <param name="changeType">Type of change</param>
    /// <param name="fullPath">Full path to file</param>
    private void AddFileToContext(string fileName, string changeType, string fullPath)
    {
        Context.Add(new FileAudit(fileName, changeType.ToUpper(), DateTime.Now, fullPath));
    }
}