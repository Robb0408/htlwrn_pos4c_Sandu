using Microsoft.EntityFrameworkCore;
using SecurityAudit.Database;

namespace SecurityAudit.Logic;

public class SecurityAudit
{
    private FileAuditContext Context { get; } = new();
    
    /// <summary>
    /// Starts a watcher which watches for changes, creations, deletions and renames.
    /// The captured events will be stored in a database.
    /// </summary>
    /// <param name="path">Path where the watcher captures activities.</param>
    /// <exception cref="DirectoryNotFoundException"></exception>
    public async Task StartWatcherAsync(string path)
    {
        if (!Directory.Exists(path))
        {
            throw new DirectoryNotFoundException();
        }
        
        FileSystemWatcher watcher = new(path);
        
        watcher.Changed += (_, eventArgs) =>
        {
            AddFileToContext(Path.GetFileName(eventArgs.FullPath), eventArgs.ChangeType.ToString(), 
                eventArgs.FullPath);
        };
        watcher.Created += (_, eventArgs) =>
        {
            AddFileToContext(Path.GetFileName(eventArgs.FullPath), eventArgs.ChangeType.ToString(), 
                eventArgs.FullPath);
        };
        watcher.Deleted += (_, eventArgs) =>
        {
            AddFileToContext(Path.GetFileName(eventArgs.FullPath), eventArgs.ChangeType.ToString(), 
                eventArgs.FullPath);
        };
        watcher.Renamed += (_, eventArgs) =>
        {
            AddFileToContext($"{Path.GetFileName(eventArgs.OldFullPath)} => " +
                             $"{Path.GetFileName(eventArgs.FullPath)}",
                eventArgs.ChangeType.ToString(),
                eventArgs.FullPath
            );
        };
        watcher.EnableRaisingEvents = true;
        Console.WriteLine($"Watcher started capturing at {path}\nPress any key to end...");
        Console.ReadKey(true);
        await Context.SaveChangesAsync();
        Console.WriteLine("Watcher stopped!\n" +
                          "View all tracked activities with the <log> command or " +
                          "clear all entries with <clean>");
        watcher.Dispose();
    }
    
    /// <summary>
    /// Gets all entries from the database.
    /// </summary>
    /// <returns>
    /// List of FileAudit entries.
    /// </returns>
    public async Task GetAllEntriesAsync()
    {
        var audits = await Context.FileAudits.ToListAsync();
        if (audits.Count > 0)
        {
            audits.ForEach(audit =>
                Console.WriteLine($"[{audit.Timestamp}] [{audit.ChangeType}] {audit.FileName}"));
        }
        else
        {
            Console.WriteLine("No entries found to get");   
        }
    }

    /// <summary>
    /// Deletes all entries from the database
    /// </summary>
    public async Task DeleteAllEntriesAsync()
    {
        if (Context.FileAudits.Any())
        {
            Context.FileAudits.RemoveRange(Context.FileAudits);
            await Context.SaveChangesAsync();
            Console.WriteLine("All entries deleted");
        }
        else
        {
            Console.WriteLine("No entries found to delete");
        }
    }

    /// <summary>
    /// Adds an entry to the database
    /// </summary>
    /// <param name="fileName">File name</param>
    /// <param name="changeType">Type of change</param>
    /// <param name="fullPath">Full path to file</param>
    private void AddFileToContext(string fileName, string changeType, string fullPath)
    {
        Context.FileAudits.Add(new FileAudit
        {
            FileName = fileName,
            ChangeType = changeType.ToUpper(),
            FullPath = fullPath
        });
    }
}