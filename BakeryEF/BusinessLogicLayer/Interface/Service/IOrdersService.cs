

using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Responce;
using DataAccessLayer.Parameters;

namespace BusinessLogicLayer.Interface.Service
{
    public interface IOrdersService
    {
        Task<IEnumerable<OrderResponce>> GetAsync(OrderParameters parameters);
        Task<IEnumerable<OrderResponce>> GetAsync();
        Task<OrderResponce> GetByIdAsync(int id);
        Task<OrderProductResponce> GetCompleteByIdAsync(int id);
        Task<IEnumerable<OrderProductResponce>> GetByClientIdAsync(int id);
        Task<IEnumerable<OrderProductResponce>> GetCompleteByIdAsync();
        Task InsertAsync(OrderRequest request);
        Task UpdateAsync(OrderRequest request);

        Task DeleteAsync(int id);

    }
}
