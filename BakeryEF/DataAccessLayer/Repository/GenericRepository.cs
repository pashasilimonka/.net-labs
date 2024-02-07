
using DataAccessLayer.Configuration;
using DataAccessLayer.Exceptions;
using DataAccessLayer.Interface.Repository;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository
{
    public abstract class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DataContext _context;
        protected readonly DbSet<TEntity> table;

        public GenericRepository(DataContext context)
        {
            _context = context;
            table = _context.Set<TEntity>();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            await Task.Run(() => table.Remove(entity));
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await table.ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await table.FindAsync(id)
                ?? throw new EntityNotFoundException(
                    GetEntityNotFoundErrorMessage(id));
        }

        public abstract Task<TEntity> GetCompleteEntityAsync(int id);
        public abstract Task<IEnumerable<TEntity>> GetCompleteEntityAsync();
        public virtual async Task InsertAsync(TEntity entity) => await table.AddAsync(entity);  

        public virtual async Task UpdateAsync(TEntity entity) => await Task.Run(() => table.Update(entity));
        protected static string GetEntityNotFoundErrorMessage(int id) =>
           $"{typeof(TEntity).Name} with id {id} not found.";
    }
}
