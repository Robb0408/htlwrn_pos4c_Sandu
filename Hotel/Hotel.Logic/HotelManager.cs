using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Logic;

public class HotelManager
{
    /// <summary>
    /// Add hotels from file to database
    /// </summary>
    /// <param name="fileName"></param>
    public async Task AddHotelsAsync(string fileName)
    {
        Console.Write("Connecting to database...");
        await using var context = new HotelContextFactory().CreateDbContext();
        Console.WriteLine("Complete");
        // Add fixed hotel specials
        Console.Write("Adding specials...");
        var specialList = new List<Special>
        {
            new() { Description = "Spa" },
            new() { Description = "Sauna" },
            new() { Description = "Dog friendly" },
            new() { Description = "Indoor pool" },
            new() { Description = "Outdoor pool" },
            new() { Description = "Bike rental" },
            new() { Description = "eCar charging station" },
            new() { Description = "Vegetarian cuisine" },
            new() { Description = "Organic food" }
        };
        await context.Specials.AddRangeAsync(specialList);
        await context.SaveChangesAsync();
        Console.WriteLine("Complete");
        
        // Add hotels from file
        var lines = await File.ReadAllLinesAsync(fileName);
        var sb = new StringBuilder();
        var hotelList = new List<string>();

        foreach (var line in lines)
        {
            if (line.Replace("\t", string.Empty) == string.Empty)
            {
                hotelList.Add(sb.ToString());
                sb.Clear();
            }
            else
            {
                sb.Append(line);
            }
        }

        // Make sure that last hotel is added -> no empty line at the end of file needed
        hotelList.Add(sb.ToString());
        sb.Clear();
        
        Console.Write("Adding hotels...");
        foreach (var hotel in hotelList)
        {
            var hotelInfo = hotel.Split("\t").Where(s => s != string.Empty).ToArray();
            var hotelName = hotelInfo[0];
            var hotelAddress = hotelInfo[1].Split(", ");
            var hotelStreet = hotelAddress[0];
            var hotelZipCode = int.Parse(hotelAddress[1][..4]);
            var hotelCity = hotelAddress[1][5..];
            var hotelSpecials = hotelInfo[2].Split(", ").ToList();
            var hotelRoomTypes = hotelInfo[4..^1];
            var hotelDisabilityFriendly = hotelInfo[^1].Contains("All");

            var newHotel = new Hotel
            {
                Name = hotelName,
                Street = hotelStreet,
                ZipCode = hotelZipCode,
                City = hotelCity
            };

            foreach (var special in hotelSpecials)
            {
                var specialEntity = await context.Specials.FirstAsync(s =>
                    string.Equals(s.Description.ToLower(), special.ToLower()));
                specialEntity.Hotels.Add(newHotel);
                newHotel.Specials.Add(specialEntity);
            }

            foreach (var roomType in hotelRoomTypes)
            {
                var roomAttributes = roomType.Split(" ");
                var roomTypeTitle = string.Join(" ", roomAttributes[4..^2]);
                var roomTypeSize = string.Join("", roomAttributes[..4]).Split("x")[1];
                var freeRooms = string.Join("", roomAttributes[..4]).Split("x")[0];
                var roomTypePrice = int.Parse(roomAttributes[^1].Replace("€", string.Empty));
                var newRoomType = new RoomType
                {
                    Title = roomTypeTitle,
                    Description = roomType,
                    Size = roomTypeSize,
                    IsDisabilityAccessible = hotelDisabilityFriendly,
                    FreeRooms = int.Parse(freeRooms)
                };
                var newPrice = new Price
                {
                    PricePerNight = roomTypePrice,
                    ValidFrom = DateTime.Now,
                    ValidTo = DateTime.Now.AddYears(1),
                    RoomType = newRoomType
                };
                newRoomType.Price = newPrice;

                newHotel.RoomTypes.Add(newRoomType);

                context.RoomTypes.Add(newRoomType);
                context.Prices.Add(newPrice);
            }

            context.Hotels.Add(newHotel);
            await context.SaveChangesAsync();
        }
        Console.WriteLine("Complete");
        Console.WriteLine("Hotels added");
    }

    /// <summary>
    /// Get everything from database
    /// </summary>
    /// <returns></returns>
    private async Task<List<Hotel>> GetHotelsAsync(string? search = null!)
    {
        await using var context = new HotelContextFactory().CreateDbContext();
        return await context.Hotels
            .Where(hotel => hotel.Name.Contains(search ?? string.Empty))
            .Include(h => h.Specials)
            .Include(h => h.RoomTypes)
            .ThenInclude(rt => rt.Price)
            .AsNoTracking()
            .ToListAsync();
    }

    /// <summary>
    /// List all hotels
    /// </summary>
    public async Task ListHotelsAsync(string? search = null!)
    {
        var hotels = await GetHotelsAsync(search);
        //check if hotels is empty
        if (hotels.Count == 0)
        {
            Console.WriteLine($"No hotels with name \"{search}\" found");
            return;
        }

        Console.Write("Write the search results into a markdown file? (y/N) ");
        var input = Console.ReadKey();
        Console.WriteLine();

        switch (input.Key)
        {
            case ConsoleKey.Enter or ConsoleKey.N:
            {
                Console.WriteLine("Listing hotels...");
                Console.WriteLine("Results:\n");
                foreach (var hotel in hotels)
                {
                    Console.WriteLine($"# {hotel.Name}");
                    Console.WriteLine();
                    Console.WriteLine("## Location");
                    Console.WriteLine();
                    Console.WriteLine($"{hotel.Street}");
                    Console.WriteLine($"{hotel.ZipCode} {hotel.City}");
                    Console.WriteLine();
                    Console.WriteLine("## Specials");
                    Console.WriteLine();
                    foreach (var special in hotel.Specials)
                    {
                        Console.WriteLine($"- {special.Description}");
                    }

                    Console.WriteLine();
                    Console.WriteLine("## Room types");
                    Console.WriteLine();
                    Console.WriteLine("| Room Type   |  Size | Price Valid From | Price Valid To | Price in € |");
                    Console.WriteLine("| ----------- | ----: | ---------------- | -------------- | ---------: |");
                    foreach (var roomType in hotel.RoomTypes)
                    {
                        Console.WriteLine(
                            $"| {roomType.Title} | {roomType.Size} | {roomType.Price.ValidFrom:dd.MM.yyyy} | {roomType.Price.ValidTo:dd.MM.yyyy} | {roomType.Price.PricePerNight} € |");
                    }

                    Console.WriteLine("---");
                    Console.WriteLine();
                }

                break;
            }
            case ConsoleKey.Y:
            {
                Console.Write("Writing to file...");
                await using StreamWriter file = new("hotels.md");
                foreach (var hotel in hotels)
                {
                    await file.WriteLineAsync($"# {hotel.Name}");
                    await file.WriteLineAsync();
                    await file.WriteLineAsync("## Location");
                    await file.WriteLineAsync();
                    await file.WriteLineAsync($"{hotel.Street}");
                    await file.WriteLineAsync($"{hotel.ZipCode} {hotel.City}");
                    await file.WriteLineAsync();
                    await file.WriteLineAsync("## Specials");
                    await file.WriteLineAsync();
                    foreach (var special in hotel.Specials)
                    {
                        await file.WriteLineAsync($"- {special.Description}");
                    }

                    await file.WriteLineAsync();
                    await file.WriteLineAsync("## Room types");
                    await file.WriteLineAsync();
                    await file.WriteLineAsync("| Room Type   |  Size | Price Valid From | Price Valid To | Price in € |");
                    await file.WriteLineAsync("| ----------- | ----: | ---------------- | -------------- | ---------: |");
                    foreach (var roomType in hotel.RoomTypes)
                    {
                        await file.WriteLineAsync(
                            $"| {roomType.Title} | {roomType.Size} | {roomType.Price.ValidFrom:dd.MM.yyyy} | {roomType.Price.ValidTo:dd.MM.yyyy} | {roomType.Price.PricePerNight} € |");
                    }

                    await file.WriteLineAsync("---");
                    await file.WriteLineAsync();
                }
                Console.WriteLine("Complete");
                Console.WriteLine("Hotels written to file \"hotels.md\"");
                break;
            }
            default:
                Console.WriteLine("Unknown command");
                break;
        }
    }

    /// <summary>
    /// Delete all hotels from database
    /// </summary>
    public async Task DeleteAllAsync()
    {
        Console.Write("Connecting to database...");
        await using var context = new HotelContextFactory().CreateDbContext();
        Console.WriteLine("Complete");
        Console.Write("Deleting all hotels...");
        context.Hotels.RemoveRange(context.Hotels);
        await context.SaveChangesAsync();
        context.Specials.RemoveRange(context.Specials);
        await context.SaveChangesAsync();
        context.RoomTypes.RemoveRange(context.RoomTypes);
        await context.SaveChangesAsync();
        context.Prices.RemoveRange(context.Prices);
        await context.SaveChangesAsync();
        Console.WriteLine("Complete");
        Console.WriteLine("All hotels deleted");
    }
}