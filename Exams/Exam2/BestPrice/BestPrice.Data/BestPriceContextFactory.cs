using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPrice.Data
{
    public class BestPriceContextFactory : IDesignTimeDbContextFactory<BestPriceContext>
    {
        public BestPriceContext CreateDbContext(string[]? args = null!)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("app-settings.json").Build();

            var optionsBuilder = new DbContextOptionsBuilder<BestPriceContext>();
            optionsBuilder
                // Uncomment the following line if you want to print generated
                // SQL statements on the console.
                // .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                .UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

            return new BestPriceContext(optionsBuilder.Options);
        }
    }
}
