using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Logic;

public class Price
{
    public int Id { get; set; }
    public RoomType RoomType { get; set; } = null!;
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    [Column(TypeName = "decimal(8,2)")]
    public decimal PricePerNight { get; set; }
}