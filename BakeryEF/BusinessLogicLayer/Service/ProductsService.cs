using AutoMapper;
using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Responce;
using BusinessLogicLayer.Interface.Service;
using DataAccessLayer.Interface;
using DataAccessLayer.Interface.Repository;
using DataAccessLayer.Model;
using DataAccessLayer.Parameters;

namespace BusinessLogicLayer.Service
{
    public class ProductsService : IProductsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        private readonly IProductsRepository _productsRepository;
        public ProductsService(IUnitOfWork unitOfWork, IMapper mapper, IProductsRepository productsRepository)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
            this._productsRepository = productsRepository;
        }
        public async Task<IEnumerable<ProductResponce>> getAllAsync()
        {
            var result = await _productsRepository.GetAsync();
            return result?.Select(mapper.Map<Product, ProductResponce>);
        }
        public async Task DeleteAsync(int id)
        {
            await _productsRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductResponce>> getWithParametersAsync(ProductParameters parameters)
        {
            var result = await _productsRepository.GetAllAsync(parameters);
            return result?.Select(mapper.Map<Product, ProductResponce>);
        }

        public async Task<ProductResponce> getByIdAsync(int id)
        {
            var result = await _productsRepository.GetByIdAsync(id);
            return mapper.Map<Product,ProductResponce>(result);
        }

        public async Task InsertAsync(ProductRequest request)
        {
            var product = mapper.Map<ProductRequest,Product>(request);
            await _productsRepository.InsertAsync(product);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductRequest request)
        {
            var product = mapper.Map<ProductRequest, Product>(request);
            await _productsRepository.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductResponce>> GetByOrder(int id)
        {
            var result = await _productsRepository.GetByOrders(id);
            return result?.Select(mapper.Map<Product, ProductResponce>);
        }
    }
}
