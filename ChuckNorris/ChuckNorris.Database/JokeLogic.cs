using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace ChuckNorris.Database;

public class JokeLogic
{
    private readonly HttpClient _client = new(); // underscore is a suggestions from rider

    /// <summary>
    /// Saves a number of jokes to the database
    /// </summary>
    /// <remarks>
    /// Stores only unique jokes. Maximum number of jokes is 10.
    /// </remarks>
    /// <param name="maxJokes"></param>
    public async Task SaveJokesAsync(int maxJokes = 5)
    {
        if (maxJokes > 10)
        {
            Console.WriteLine("Maximum number of jokes is 10. Aborting.");
            return;
        }

        await using var context = new JokeContextFactory().CreateDbContext();
        var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            while (context.Jokes.Count() < maxJokes)
            {
                var retries = 0;
                var joke = await GetRandomJokeAsync();
                //check if joke already exists in database
                while (context.Jokes.Any(j => j.Id == joke!.Id) && retries < 10)
                {
                    Console.WriteLine("Joke already exists in database. Fetching new joke. Retries: " + retries);
                    joke = await GetRandomJokeAsync();
                    retries++;
                }

                if (retries == 10)
                {
                    Console.WriteLine("Could not find anymore unique jokes. Stopping.");
                    break;
                }

                context.Jokes.Add(joke!);
                await context.SaveChangesAsync();
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Error while saving joke to database.");
            await transaction.RollbackAsync();
            throw;
        }

        Console.WriteLine($"Saved {maxJokes} jokes to database.");
        await transaction.CommitAsync();
    }

    /// <summary>
    /// Lists all jokes from the database
    /// </summary>
    public async Task ListJokesAsync()
    {
        await using var context = new JokeContextFactory().CreateDbContext();
        var jokes = await context.Jokes.AsNoTracking().ToListAsync();
        foreach (var joke in jokes)
        {
            Console.WriteLine($"{joke.Id}. {joke.JokeValue}");
        }
    }

    /// <summary>
    /// Deletes all jokes from the database
    /// </summary>
    public async Task DeleteJokesAsync()
    {
        await using var context = new JokeContextFactory().CreateDbContext();
        var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            await context.Database.ExecuteSqlAsync($"DELETE FROM Joke");
            await context.SaveChangesAsync();
        }
        catch (Exception)
        {
            Console.WriteLine("Error while deleting jokes from database.");
            await transaction.RollbackAsync();
            throw;
        }

        Console.WriteLine($"Deleted {context.Jokes.Count()} jokes from database.");
        await transaction.CommitAsync();
    }

    /// <summary>
    /// Fetches a random joke from the Chuck Norris API
    /// </summary>
    /// <returns>A random joke</returns>
    private async Task<Joke?> GetRandomJokeAsync()
    {
        try
        {
            // Make sure that no explicit jokes are fetched
            JokeDummy jokeDummy;
            do
            {
                var response = await _client.GetAsync("https://api.chucknorris.io/jokes/random");
                response.EnsureSuccessStatusCode();
                jokeDummy = JsonSerializer.Deserialize<JokeDummy>(await response.Content.ReadAsStringAsync())!;
            } while (jokeDummy.Categories.Contains("explicit"));
            
            return new Joke
            {
                ChuckNorrisId = jokeDummy.Id,
                Url = jokeDummy.Url,
                JokeValue = jokeDummy.Value
            };
        }
        catch (HttpRequestException)
        {
            await Console.Error.WriteLineAsync("Error while fetching joke from API.");
            return null;
        }
    }
}