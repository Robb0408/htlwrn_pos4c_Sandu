using Microsoft.EntityFrameworkCore;

namespace SecurityAudit.Database;

public class FileAuditContext : DbContext
{
    public DbSet<FileAudit> FileAudits { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=../SecurityAudit.Database/SecurityAudit.db");
    }
}