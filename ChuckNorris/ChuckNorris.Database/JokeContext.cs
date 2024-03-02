using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace ChuckNorris.Database;

public class JokeContext : DbContext
{
    public DbSet<Joke> Jokes { get; set; } = null!;
    
    public JokeContext(DbContextOptions<JokeContext> options) : base(options) { }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove(typeof(TableNameFromDbSetConvention));
    }
}