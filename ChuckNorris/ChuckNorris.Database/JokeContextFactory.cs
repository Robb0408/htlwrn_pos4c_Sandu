using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ChuckNorris.Database;

// This factory is responsible for creating our DB context. Note that
// this will NOT BE NECESSARY anymore once we move to ASP.NET.
public class JokeContextFactory : IDesignTimeDbContextFactory<JokeContext>
{
    public JokeContext CreateDbContext(string[]? args = null!)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("app-settings.json").Build();

        var optionsBuilder = new DbContextOptionsBuilder<JokeContext>();
        optionsBuilder
            // Uncomment the following line if you want to print generated
            // SQL statements on the console.
            // .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
            .UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

        return new JokeContext(optionsBuilder.Options);
    }
}