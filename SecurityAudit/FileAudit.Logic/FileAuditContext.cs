using System.IO;
using Microsoft.EntityFrameworkCore;

namespace FileAudit.Logic
{
	public class FileAuditContext : DbContext
	{
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={Directory.GetCurrentDirectory()}/SecurityAudit.db");
        }

		public DbSet<FileAudit> FileAudits => null!;
	}
}

