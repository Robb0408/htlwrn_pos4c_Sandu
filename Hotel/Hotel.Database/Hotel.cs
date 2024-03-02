using System.ComponentModel.DataAnnotations;

namespace Hotel.Logic;

public class Hotel
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; } = null!;
    [MaxLength(100)]
    public string Street { get; set; } = null!;
    public int ZipCode { get; set; }
    [MaxLength(100)]
    public string City { get; set; } = null!;
    
    public List<Special> Specials { get; set; } = [];
    public List<RoomType> RoomTypes { get; set; } = [];
    public List<Booking> Bookings { get; set; } = [];
}