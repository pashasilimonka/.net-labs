using DataAccessLayer.Model;
using DataAccessLayer.Pagination;
using DataAccessLayer.Parameters;

namespace DataAccessLayer.Interface.Repository
{
    public interface IOrdersRepository : IRepository<Order>
    {
        Task<PagedList<Order>> GetAllAsync(OrderParameters parameters);
        Task<IEnumerable<Order>> GetByClientAsync(int id);
    }
}
