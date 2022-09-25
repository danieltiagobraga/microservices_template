using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.Db
{
    public class CustomersDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public CustomersDbContext(DbContextOptions options) : base(options)
        {
            SeedData();
        }

        private void SeedData()
        {
            if (!Customers.Any())
            {
                Customers.Add(new Db.Customer() { Id = 1, Name = "Johnnie Walker", Address = "10 Elm St." });
                Customers.Add(new Db.Customer() { Id = 2, Name = "John Due", Address = "45 Main St." });
                Customers.Add(new Db.Customer() { Id = 3, Name = "William Lawson", Address = "100 10th St." });

                SaveChanges();
            }
        }
    }
}