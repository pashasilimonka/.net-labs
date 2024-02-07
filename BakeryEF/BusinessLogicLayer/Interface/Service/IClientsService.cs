

using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Responce;
using DataAccessLayer.Parameters;

namespace BusinessLogicLayer.Interface.Service
{
    public interface IClientsService
    {
        Task<IEnumerable<ClientResponce>> GetAsync();
        Task<IEnumerable<ClientResponce>> GetAllAsync(ClientParameters parameters);

        Task<ClientResponce> GetByIdAsync(int id);
        Task<ClientOrders> GetCompleteByIdAsync(int id);

        Task<IEnumerable<ClientOrders>> GetCompleteByIdAsync();
        Task InsertAsync(ClientRequest entity);

        Task UpdateAsync(ClientRequest entity);

        Task DeleteAsync(int id);
    }
}
