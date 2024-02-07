
using DataAccessLayer.Interface.Repository;

namespace DataAccessLayer.Interface
{
    public interface IUnitOfWork
    {
        IClientsRepository _clientsRepository { get; }
        IOrdersRepository _ordersRepository { get; }
        IProductsRepository _productsRepository { get; }
        Task SaveChangesAsync();
    }
}
