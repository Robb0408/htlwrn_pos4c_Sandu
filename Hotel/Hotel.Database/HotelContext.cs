using Microsoft.EntityFrameworkCore;

namespace Hotel.Logic;

public class HotelContext : DbContext
{
    public DbSet<Hotel> Hotels { get; set; } = null!;
    public DbSet<Special> Specials { get; set; } = null!;
    public DbSet<RoomType> RoomTypes { get; set; } = null!;
    public DbSet<Price> Prices { get; set; } = null!;
    public DbSet<Booking> Bookings { get; set; } = null!;

    public HotelContext(DbContextOptions<HotelContext> options) : base(options) { }
}