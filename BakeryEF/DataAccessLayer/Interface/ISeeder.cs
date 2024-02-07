using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Interface
{
    public interface ISeeder<T> where T : class
    {
        void Seed(EntityTypeBuilder<T> builder);
    }
}
