using System.ComponentModel.DataAnnotations;

namespace Hotel.Logic;

public class Special
{
    public int Id { get; set; }
    [MaxLength(500)]
    public string Description { get; set; } = null!;
    
    public List<Hotel> Hotels { get; set; } = [];
}