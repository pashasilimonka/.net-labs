using AutoMapper;
using Azure.Core;
using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Responce;
using BusinessLogicLayer.Interface.Service;
using DataAccessLayer.Interface;
using DataAccessLayer.Interface.Repository;
using DataAccessLayer.Model;
using DataAccessLayer.Parameters;

namespace BusinessLogicLayer.Service
{

    public class ClientsService : IClientsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        private readonly IClientsRepository _clientsRepository;

        public ClientsService(IUnitOfWork unitOfWork, IMapper mapper, IClientsRepository clientsRepository)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
            _clientsRepository = clientsRepository;
        }

        public async Task DeleteAsync(int id)
        {
            await _clientsRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }


        public async Task<IEnumerable<ClientResponce>> GetAllAsync(ClientParameters parameters)
        {
            var result = await _clientsRepository.GetAllAsync(parameters);
            return result?.Select(mapper.Map<Client, ClientResponce>);
        }

        public async Task<IEnumerable<ClientResponce>> GetAsync()
        {
            var result = await _clientsRepository.GetAsync();
            return result?.Select(mapper.Map<Client, ClientResponce>);
        }

        public async Task<ClientResponce> GetByIdAsync(int id)
        {
            var result  = await _clientsRepository.GetByIdAsync(id);
            return mapper.Map<Client, ClientResponce>(result);
        }

        public async Task<ClientOrders> GetCompleteByIdAsync(int id)
        {
            var result = await _clientsRepository.GetCompleteEntityAsync(id);
            return mapper.Map<Client,ClientOrders>(result);
        }
        public async Task<IEnumerable<ClientOrders>> GetCompleteByIdAsync()
        {
            var result = await _clientsRepository.GetCompleteEntityAsync();
            return result?.Select(mapper.Map<Client, ClientOrders>);
        }


        public async Task InsertAsync(ClientRequest entity)
        {
            var client = mapper.Map<ClientRequest, Client>(entity);
            await _clientsRepository.InsertAsync(client);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(ClientRequest entity)
        {
            var client = mapper.Map<ClientRequest, Client>(entity);
            await _clientsRepository.UpdateAsync(client);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
