using Microsoft.EntityFrameworkCore;

namespace FileAudit.Logic
{
	public class FileAuditContext : DbContext
	{
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase("FileAuditDB");
        }

		public DbSet<FileAudit> FileAudits { get; set; } = null!;
    }
}

