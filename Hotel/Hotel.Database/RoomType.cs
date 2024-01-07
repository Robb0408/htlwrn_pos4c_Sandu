using System.ComponentModel.DataAnnotations;

namespace Hotel.Logic;

public class RoomType
{
    public int Id { get; set; }
    public int HotelId { get; set; }
    public Hotel Hotel { get; set; } = null!;
    [MaxLength(150)]
    public string Title { get; set; } = null!;
    [MaxLength(500)]
    public string Description { get; set; } = null!;
    [MaxLength(20)]
    public string Size { get; set; } = null!;
    public bool IsDisabilityAccessible { get; set; }
    public int FreeRooms { get; set; }
    public Price Price { get; set; } = null!;
    public int PriceId { get; set; }
    public Booking? Booking { get; set; }
    
}