

using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces.Repository
{
    public interface IDistributorsRepository
    {
        Task<IEnumerable<Distributor>> getAllDistributors();
        Task<Distributor> getDistributor(int id);
        Task<int> createDistributor(Distributor distributor);
        Task<int> updateDistributor(int id,Distributor distributor);
        Task<int> deleteDistributor(int id);
        Task<IEnumerable<Distributor>> getAllIngredientsAndDistributors();
    }
}
