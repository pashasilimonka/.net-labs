
using DataAccessLayer.Model;
using DataAccessLayer.Seeder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace DataAccessLayer.Configuration
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .HasKey(o => o.order_id);
            builder
                .Property(o =>o.summary_price)
                .IsRequired();
            builder
                .Property(o => o.receivedAt)
                .IsRequired();
            builder
                .HasMany(o => o.products)
                .WithMany(p => p.orders)
                .UsingEntity(
                "OrderProduct",
                p => p.HasOne(typeof(Product)).WithMany().HasForeignKey("product_id").HasPrincipalKey(nameof(Product.product_id)),
                o => o.HasOne(typeof(Order)).WithMany().HasForeignKey("order_id").HasPrincipalKey(nameof(Order.order_id)),
                j => j.HasKey("product_id", "order_id"));

            builder
                .HasOne(c => c.client)
                .WithMany(c => c.orders)
                .HasForeignKey(c => c.client_id)
                .IsRequired();

            new OrderSeeder().Seed(builder);
        }
    }
}
