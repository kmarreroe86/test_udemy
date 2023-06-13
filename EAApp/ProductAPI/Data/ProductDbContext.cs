using Microsoft.EntityFrameworkCore;

namespace ProductAPI.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var products =
                    new List<Product>()
                    {
                        new Product()
                        {
                            Id = 1,
                            Name = "Keyboard",
                            Description = "Gaming Keyboard with lights",
                            Price = 150,
                            ProductType = ProductType.PERIPHERALS
                        },
                        new Product()
                        {
                            Id = 2,
                            Name = "Mouse",
                            Description = "Gaming Mouse",
                            Price = 40,
                            ProductType = ProductType.PERIPHERALS
                        },
                        new Product()
                        {
                            Id = 3,
                            Name = "Monitor",
                            Description = "HD monitor",
                            Price = 400,
                            ProductType = ProductType.MONITOR
                        },
                        new Product()
                        {
                            Id = 4,
                            Name = "Cabinet",
                            ProductType = ProductType.EXTERNAL,
                            Description = "Business Cabinet with lights",
                            Price = 60
                        }
                    };
            modelBuilder.Entity<Product>().HasData(products);
        }
    }
}
