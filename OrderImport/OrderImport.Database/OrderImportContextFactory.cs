using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace OrderImport.Database;

// This factory is responsible for creating our DB context. Note that
// this will NOT BE NECESSARY anymore once we move to ASP.NET.
public class OrderImportContextFactory : IDesignTimeDbContextFactory<OrderImportContext>
{
    public OrderImportContext CreateDbContext(string[]? args = null)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("app-settings.json").Build();

        var optionsBuilder = new DbContextOptionsBuilder<OrderImportContext>();
        optionsBuilder
            // Uncomment the following line if you want to print generated
            // SQL statements on the console.
            // .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
            .UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

        return new OrderImportContext(optionsBuilder.Options);
    }
}