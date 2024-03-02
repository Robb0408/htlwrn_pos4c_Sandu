using Hotel.Logic;

HotelManager manager = new();
if (args.Length == 0)
{
    Console.WriteLine("Usage: Hotel.Console [--add] [--search <name>] [--list] [--delete]");
    return;
}
switch (args[0])
{
    case "--add":
        await manager.AddHotelsAsync("hotels.txt");
        break;
    case "--search" when args.Length == 2:
        await manager.ListHotelsAsync(args[1]);
        break;
    case "--list":
        await manager.ListHotelsAsync();
        break;
    case "--delete":
        await manager.DeleteAllAsync();
        break;
    default:
        Console.WriteLine("Unknown command");
        break;
}