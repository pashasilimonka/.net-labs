

using DataAccessLayer.Configuration;
using DataAccessLayer.Interface;
using DataAccessLayer.Interface.Repository;

namespace DataAccessLayer.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContextl;
        public IClientsRepository _clientsRepository { get; }
        public IOrdersRepository _ordersRepository { get; }
        public IProductsRepository _productsRepository { get; }

        public UnitOfWork(IClientsRepository clientsRepository,
            IOrdersRepository ordersRepository,
            DataContext dataContext) { 
            _clientsRepository = clientsRepository;
            _ordersRepository = ordersRepository;
            _dataContextl = dataContext;
        }
        public async Task SaveChangesAsync()
        {
             await _dataContextl.SaveChangesAsync();
        }
    }
}
