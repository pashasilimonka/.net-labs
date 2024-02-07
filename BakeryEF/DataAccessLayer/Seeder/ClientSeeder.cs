
using DataAccessLayer.Interface;
using DataAccessLayer.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Seeder
{
    public class ClientSeeder : ISeeder<Client>
    {
        private static readonly List<Client> _clients = new()
        {
            new Client {
                client_id = 1,
                name = "Pasha",
                surname = "Sylymonka",
                phone_number = "1234567890"
            }
        };
        public void Seed(EntityTypeBuilder<Client> builder) => builder.HasData(_clients);
    }
}
