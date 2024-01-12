using System.Text.Json;

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
    public async Task SaveJokeAsync(int maxJokes = 5)
    {
        if (maxJokes > 10)
        {
            Console.WriteLine("Max jokes cannot be more than 10. Setting to 10.");
            maxJokes = 10;
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
        finally
        {
            Console.WriteLine($"Saved {context.Jokes.Count()} jokes to database.");
            await transaction.CommitAsync();
        }

    }
    /// <summary>
    /// Fetches a random joke from the Chuck Norris API
    /// </summary>
    /// <returns>A random joke</returns>
    private async Task<Joke?> GetRandomJokeAsync()
    {
        try
        {
            var response = await _client.GetAsync("https://api.chucknorris.io/jokes/random");
            response.EnsureSuccessStatusCode();
            var jokeDummy = JsonSerializer.Deserialize<JokeDummy>(await response.Content.ReadAsStringAsync())!;
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