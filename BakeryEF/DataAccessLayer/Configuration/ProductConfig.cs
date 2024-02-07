
using DataAccessLayer.Model;
using DataAccessLayer.Seeder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace DataAccessLayer.Configuration
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder
                .HasKey(p => p.product_id);

            builder
                .Property(p => p.name)
                .HasMaxLength(50)
                .IsRequired();
            builder
                .Property(p => p.description)
                .HasMaxLength(500)
                .IsRequired();
            builder
                .Property(p => p.price)
                .IsRequired();
            new ProductSeeder().Seed(builder);
        }
    }
}
