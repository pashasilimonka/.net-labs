using AutoMapper;
using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Responce;
using BusinessLogicLayer.Interface.Service;
using DataAccessLayer.Interface;
using DataAccessLayer.Interface.Repository;
using DataAccessLayer.Model;
using DataAccessLayer.Parameters;
using System.Runtime.CompilerServices;

namespace BusinessLogicLayer.Service
{
    public class OrdersService :IOrdersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        private readonly IOrdersRepository _ordersRepository;

        public OrdersService(IUnitOfWork unitOfWork, IMapper mapper, IOrdersRepository ordersRepository)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
            this._ordersRepository = ordersRepository;
        }

        public async Task DeleteAsync(int id)
        {
            await _ordersRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<IEnumerable<OrderResponce>> GetAsync()
        {
            var result = await _ordersRepository.GetAsync();
            return result?.Select(mapper.Map<Order, OrderResponce>);

        }
        public async Task<IEnumerable<OrderResponce>> GetAsync(OrderParameters parameters)
        {
            var result = await _ordersRepository.GetAllAsync(parameters);
            return result?.Select(mapper.Map<Order, OrderResponce>);
        }

        public async Task<IEnumerable<OrderProductResponce>> GetByClientIdAsync(int id)
        {
            var result = await _ordersRepository.GetByClientAsync(id);
            return result?.Select(mapper.Map<Order, OrderProductResponce>); ;
        }

        public async Task<OrderResponce> GetByIdAsync(int id)
        {
            var result = await _ordersRepository.GetByIdAsync(id);
            return mapper.Map<Order, OrderResponce>(result);
        }

        public async Task<OrderProductResponce> GetCompleteByIdAsync(int id)
        {
            var result = await _ordersRepository.GetCompleteEntityAsync(id);
            return mapper.Map<Order, OrderProductResponce>(result);
        }
        public async Task<IEnumerable<OrderProductResponce>> GetCompleteByIdAsync()
        {
            var result = await _ordersRepository.GetCompleteEntityAsync();
            return result?.Select(mapper.Map<Order, OrderProductResponce>);
        }

        public async Task InsertAsync(OrderRequest request)
        {
            var order = mapper.Map<OrderRequest, Order>(request);
            await _ordersRepository.InsertAsync(order);
            _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(OrderRequest request)
        {
            var order = mapper.Map<OrderRequest,Order>(request);
            await _ordersRepository.UpdateAsync(order);
            _unitOfWork.SaveChangesAsync();
        }
    }
}
