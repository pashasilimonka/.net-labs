
using DataAccessLayer.Configuration;
using DataAccessLayer.Exceptions;
using DataAccessLayer.Interface.Repository;
using DataAccessLayer.Model;
using DataAccessLayer.Pagination;
using DataAccessLayer.Parameters;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace DataAccessLayer.Repository
{
    public class ClientsRepository : GenericRepository<Client>,IClientsRepository
    {
        public ClientsRepository(DataContext context) : base(context) { }

        public async Task<PagedList<Client>> GetAllAsync(ClientParameters parameters)
        {
            IQueryable<Client> clients = table;
            SearchByName(ref clients, parameters.name);
            SearchBySurname(ref clients, parameters.surname);
            SearchByPhoneNumber(ref clients, parameters.phone_number);
            return await PagedList<Client>.ToPagedListAsync(
                clients,
                parameters.PageNumber,
                parameters.PageSize
                );
        }

        public override async Task<Client> GetCompleteEntityAsync(int id)
        {
            var result = await table.Include(client => client.orders)
                .SingleOrDefaultAsync(client => client.client_id == id);
            return result ?? throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(id));
        }
        public override async Task<IEnumerable<Client>> GetCompleteEntityAsync()
        {
            var result = await table.Include(client => client.orders).ToListAsync();
                
            return result;
        }
        public static void SearchByName(ref IQueryable<Client> clients, string? name) {

            if (name == null)
            {
                return;
            }
            clients = clients.Where(client => client.name == name);
        }
        public static void SearchBySurname(ref IQueryable<Client> clients, string? surname)
        {
            if (surname == null)
            {
                return;
            }
             clients  = clients.Where(client => client.surname == surname);
        }
        public static void SearchByPhoneNumber(ref IQueryable<Client> clients, string? phoneNumber)
        {
            if (phoneNumber == null) {
                return;
            }
            clients = clients.Where(client => client.phone_number == phoneNumber);
        }

    }
}
