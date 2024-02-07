

using DataAccessLayer.Model;
using DataAccessLayer.Pagination;
using DataAccessLayer.Parameters;

namespace DataAccessLayer.Interface.Repository
{
    public interface IProductsRepository:IRepository<Product>
    {
        Task<PagedList<Product>> GetAllAsync(ProductParameters parameters);
        Task<IEnumerable<Product>> GetByOrders(int orderId);
    }
}
