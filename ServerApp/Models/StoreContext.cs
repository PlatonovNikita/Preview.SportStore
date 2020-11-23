using Microsoft.EntityFrameworkCore;

namespace ServerApp.Models
{
    public class StoreContext: DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options) { }
    }
}