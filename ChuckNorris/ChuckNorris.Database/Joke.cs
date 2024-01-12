using System.ComponentModel.DataAnnotations;

namespace ChuckNorris.Database;

public class Joke
{
    public int Id { get; set; }
    [MaxLength(40)]
    public string ChuckNorrisId { get; set; } = null!;
    [MaxLength(1024)]
    public string Url { get; set; } = null!;
    public string JokeValue { get; set; } = null!;
}