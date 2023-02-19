using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WarehouseProject.Model;

namespace WarehouseProject
{
    public class WarehouseContext : DbContext
    {
        private readonly string connectionString = "Data Source=DESKTOP-Q7DNDBI;Initial Catalog=Warehouse;Integrated Security=True;Encrypt=False";

        public WarehouseContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

    }
}
