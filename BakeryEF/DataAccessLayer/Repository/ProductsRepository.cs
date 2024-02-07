
using DataAccessLayer.Configuration;
using DataAccessLayer.Interface.Repository;
using DataAccessLayer.Model;
using DataAccessLayer.Pagination;
using DataAccessLayer.Parameters;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository
{
    public class ProductsRepository : GenericRepository<Product>, IProductsRepository
    {
        public ProductsRepository(DataContext dataContext) : base(dataContext) { }
        public async Task<PagedList<Product>> GetAllAsync(ProductParameters parameters)
        {
            IQueryable<Product> products = table.Include(product => product.orders);
            SearchByName(ref products, parameters.name);
            SearchByDescription(ref products,parameters.description);
            SearchByPrice(ref products, parameters.price);
            return await  PagedList<Product>.ToPagedListAsync(products,
                parameters.PageNumber,
                parameters.PageSize);


        }

        public override Task<Product> GetCompleteEntityAsync(int id)
        {
            throw new NotImplementedException();
        }
        public static void SearchByName(ref IQueryable<Product> products, string name)
        {
            if(name == null)
            {
                return;
            }
            products = products.Where(product => product.name == name);
        }

        public static void SearchByDescription(ref IQueryable<Product> products, string description)
        {
            if(description == null)
            {
                return;
            }
            products = products.Where(product => product.description == description);
        }
        public static void SearchByPrice(ref IQueryable<Product> products, float? price)
        {
            if(price == null)
            {
                return;
            }
            products = products.Where(product => product.price == price);
        }

        public override Task<IEnumerable<Product>> GetCompleteEntityAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetByOrders(int orderId)
        {
            var result = await _context.orders
                .Where(o => o.order_id == orderId)
                .SelectMany(o => o.products)
                .ToListAsync();
            return result;
        }
    }
}
