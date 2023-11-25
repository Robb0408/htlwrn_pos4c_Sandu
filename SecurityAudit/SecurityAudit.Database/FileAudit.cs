using System.ComponentModel.DataAnnotations;

namespace SecurityAudit.Database;

public class FileAudit
{
    public int Id { get; set; }
    
    [MaxLength(250)]
    public string FileName { get; set; } = null!;
    
    [MaxLength(50)]
    public string ChangeType { get; set; } = null!;
    
    public DateTime Timestamp { get; set; } = DateTime.Now;
    
    [MaxLength(500)]
    public string FullPath { get; set; } = null!;
}