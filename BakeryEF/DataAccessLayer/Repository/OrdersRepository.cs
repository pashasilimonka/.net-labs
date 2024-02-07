
using DataAccessLayer.Configuration;
using DataAccessLayer.Exceptions;
using DataAccessLayer.Interface.Repository;
using DataAccessLayer.Model;
using DataAccessLayer.Pagination;
using DataAccessLayer.Parameters;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository
{
    public class OrdersRepository : GenericRepository<Order>, IOrdersRepository
    {
        public OrdersRepository(DataContext context): base(context) { }
        public async Task<PagedList<Order>> GetAllAsync(OrderParameters parameters)
        {
            IQueryable<Order> orders = table.Include(order => order.client);
            SearchByPrice(ref orders, parameters.summary_price);
            SearchByOrderTime(ref orders, parameters.orderedAt);
            SearchByReceiveTime(ref orders, parameters.receivedAt);
            SearchByClient(ref orders, parameters.client_id);
            return await PagedList<Order>.ToPagedListAsync(
                orders,
                parameters.PageNumber,
                parameters.PageSize
                );
        }

        public override async Task<Order> GetCompleteEntityAsync(int id)
        {
            var result = await table.Include(order => order.products)
                .SingleOrDefaultAsync(order => order.order_id == id);
            return result??throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(id));
        }
        public override async Task<IEnumerable<Order>> GetCompleteEntityAsync()
        {
            var result = await table.Include(order => order.products).ToListAsync();    
                return result;
        }
        public static void SearchByPrice(ref IQueryable<Order> orders, float? summary_price)
        {
            if (summary_price == null)
            {
                return;
            }
            orders = orders.Where(client => client.summary_price == summary_price);
        }
        public static void SearchByOrderTime(ref IQueryable<Order> orders, DateTime? orderedAt)
        {
            if (orderedAt == null)
            {
                return;
            }
            orders = orders.Where(client => client.orderedAt == orderedAt);
        }
        public static void SearchByReceiveTime(ref IQueryable<Order> orders, DateTime? receivedAt)
        {
            if (receivedAt == null)
            {
                return;
            }
            orders = orders.Where(client => client.receivedAt == receivedAt);
        }
        public static void SearchByClient(ref IQueryable<Order> orders, int? client_id)
        {
            if (client_id == null)
            {
                return;
            }
            orders = orders.Where(order => order.client_id == client_id);
        }

        public async Task<IEnumerable<Order>> GetByClientAsync(int id)
        {
            IQueryable<Order> result = table.Include(o => o.client)
                .Include(o=>o.products);
            SearchByClient(ref result, id);
                
             
            return result;
        }
    }
}
