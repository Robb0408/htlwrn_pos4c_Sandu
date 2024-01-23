using ChuckNorris.Database;

JokeLogic jokeLogic = new();
var command = args.Length > 0 ? args[0] : "";

switch (command)
{
    case "--save":
        if (args.Length > 1 && int.TryParse(args[1], out var maxJokes))
            await jokeLogic.SaveJokesAsync(maxJokes);
        else
            await jokeLogic.SaveJokesAsync();
        break;
    case "--list":
        await jokeLogic.ListJokesAsync();
        break;
    case "--delete":
        await jokeLogic.DeleteJokesAsync();
        break;
    default:
        // print usage of program
        Console.WriteLine("Usage: dotnet run -- [command]");
        Console.WriteLine("Commands:");
        Console.WriteLine("--save: Saves jokes to database");
        Console.WriteLine("--list: Lists all jokes in database");
        Console.WriteLine("--delete: Deletes all jokes in database");
        break;
}