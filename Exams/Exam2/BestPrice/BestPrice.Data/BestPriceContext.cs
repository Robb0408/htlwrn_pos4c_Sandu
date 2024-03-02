using BestPrice.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPrice.Data
{
    public class BestPriceContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Availability> Availability { get; set; } = null!;
        public DbSet<SpecialOffer> SpecialOffer { get; set; } = null!;
        public DbSet<Vendor> Vendors { get; set; } = null!;

        public BestPriceContext(DbContextOptions<BestPriceContext> options) : base(options) { }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Remove(typeof(TableNameFromDbSetConvention));
            configurationBuilder.Conventions.Remove(typeof(CascadeDeleteConvention));
            // HAU: ℹ️ just a hint: for ms SqlServer also the SqlServerOnDeleteConvention has to be removed
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>()
                .HasIndex(u => u.Name)
                .IsUnique();

            builder.Entity<Vendor>()
                .HasIndex(u => u.Name)
                .IsUnique();
        }
    }
}
