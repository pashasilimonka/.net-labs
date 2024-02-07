using DataAccessLayer.Interface;
using DataAccessLayer.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Seeder
{
    public class ProductSeeder : ISeeder<Product>
    {
        private static readonly List<Product> _products = new()
        {
            new Product
            {
                product_id = 1,
                name = "Bulka",
                description = "Wery sweet chocolate",
                price = 50
            }
        };
        public void Seed(EntityTypeBuilder<Product> builder) => builder.HasData(_products);
    }
}
