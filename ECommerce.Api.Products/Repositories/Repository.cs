using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerce.Api.Products.Repositories
{
    public class Repository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        private readonly DbSet<TEntity> _entities;

        public Repository(DbContext context)
        {
            Context = context;
            _entities = Context.Set<TEntity>();
        }

        public ValueTask<TEntity?> GetAsync(int id) => 
            _entities.FindAsync(id);

        public Task<List<TEntity>> GetAllAsync() =>
            _entities.ToListAsync();

        public Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) =>
            _entities.SingleOrDefaultAsync(predicate);

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        public void Add(TEntity entity)
        {
            _entities.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }
    }
}
