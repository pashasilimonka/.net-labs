using DataAccessLayer.Interface;
using DataAccessLayer.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data;

namespace DataAccessLayer.Seeder { 
    public class OrderSeeder : ISeeder<Order>
    {
        private static readonly List<Order> _orders = new()
        {
            new Order
            {
                order_id = 1,
                summary_price = 65,
                orderedAt = DateTime.Now,
                receivedAt = DateTime.Now
            }
        };
        public void Seed(EntityTypeBuilder<Order> builder) => builder.HasData(_orders);
    }
}
