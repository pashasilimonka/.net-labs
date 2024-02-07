
using DataAccessLayer.Model;
using Microsoft.EntityFrameworkCore;


namespace DataAccessLayer.Configuration
{
    public class DataContext : DbContext
    { 
       public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Client> clients { get; set; }
        
        public DbSet<Order> orders { get; set; }
        public DbSet<Product> products { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ClientConfig());
            modelBuilder.ApplyConfiguration(new OrderConfig());
            modelBuilder.ApplyConfiguration(new ProductConfig());
            
        }

       

    }
}
