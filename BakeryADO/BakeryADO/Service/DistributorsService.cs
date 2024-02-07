

using BakeryADO.Entities;
using BakeryADO.Interfaces.Repository;
using BakeryADO.Interfaces.Service;


namespace BakeryADO.Service
{
    public class DistributorsService : IDistributorsService
    {
        private readonly IDistributorsRepository _repository;
        public DistributorsService(IDistributorsRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> createDistributor(Distributor distributor)
        {
            return await _repository.createDistributor(distributor);
        }

        public async Task<int> deleteDistributor(int id)
        {
           return await (_repository.deleteDistributor(id));
        }

        public async Task<IEnumerable<Distributor>> getAllDistributors()
        {
            return await _repository.getAllDistributors();
        }

        public async Task<Distributor> getDistributor(int id)
        {
            return await _repository.getDistributor(id);
        }

        public async Task<int> updateDistributor(int id, Distributor distributor)
        {
            return await (_repository.updateDistributor(id, distributor));
        }
        public async Task<IEnumerable<Distributor>> getAllIngredientsAndDistributors()
        {
            return await _repository.getAllIngredientsAndDistributors();
        }
    }
}
