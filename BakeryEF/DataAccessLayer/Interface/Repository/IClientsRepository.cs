
using DataAccessLayer.Model;
using DataAccessLayer.Pagination;
using DataAccessLayer.Parameters;

namespace DataAccessLayer.Interface.Repository
{
    public interface IClientsRepository : IRepository<Client>
    {
        Task<PagedList<Client>> GetAllAsync(ClientParameters parameters);
    }
}
