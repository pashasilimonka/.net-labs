
using DataAccessLayer.Model;
using DataAccessLayer.Seeder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace DataAccessLayer.Configuration
{
    public class ClientConfig : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder
                .HasKey(c => c.client_id);
            builder
                .Property(c => c.name)
                .HasMaxLength(50)
                .IsRequired();
            builder
                .Property(c => c.name)
                .HasMaxLength(50)
                .IsRequired();
            

            new ClientSeeder().Seed(builder);

        }
    }
}
