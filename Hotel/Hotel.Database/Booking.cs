using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Logic;

public class Booking
{
    public int Id { get; set; }
    public Hotel Hotel { get; set; } = null!;
    public int HotelId { get; set; }
    public RoomType? RoomType { get; set; }
    public int? RoomTypeId { get; set; } //made nullable to bypass cycles or multiple cascade paths
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    [MaxLength(100)]
    public string FirstName { get; set; } = null!;
    [MaxLength(100)]
    public string LastName { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    [Column(TypeName = "decimal(8,2)")]
    public decimal EstimatedPrice { get; set; }
}