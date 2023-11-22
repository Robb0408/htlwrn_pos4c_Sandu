using System.ComponentModel.DataAnnotations;

namespace FileAudit.Logic;

public class FileAudit
{
    public int Id { get; set; }

    [MaxLength(250)]
    public string FileName { get; set; } = null!;

    [MaxLength(50)]
    public string ChangeType { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    [MaxLength(500)]
    public string FullPath { get; set; } = null!;
}
