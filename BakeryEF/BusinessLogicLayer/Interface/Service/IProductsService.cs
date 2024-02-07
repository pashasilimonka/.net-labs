

using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Responce;
using DataAccessLayer.Parameters;

namespace BusinessLogicLayer.Interface.Service
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductResponce>> getAllAsync();
        Task<IEnumerable<ProductResponce>> getWithParametersAsync(ProductParameters parameters);
        Task<ProductResponce> getByIdAsync(int id);
        Task InsertAsync(ProductRequest request);
        Task UpdateAsync(ProductRequest request);
        Task DeleteAsync(int id);
        Task<IEnumerable<ProductResponce>> GetByOrder(int id);
    }
}
