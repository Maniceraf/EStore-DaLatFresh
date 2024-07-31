using Microsoft.EntityFrameworkCore;
using WebStore.Data;
using WebStore.Interfaces;

namespace WebStore.Implements
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext Context;
        protected readonly DbSet<T> DbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            DbSet = Context.Set<T>();
        }

        public T? GetById(int id)
        {
            return DbSet.Find(id);
        }

        public List<T> GetAll()
        {
            return DbSet.ToList();
        }

        public virtual void Create(T entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
        }

        public virtual void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public virtual void DeleteRange(List<T> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }
        public virtual async Task CreateAsync(T entity)
        {
            await DbSet.AddAsync(entity);
        }

        public virtual Task UpdateAsync(T entity)
        {
            return Task.FromResult(0);
        }

        public virtual Task UpdateRangeAsync(List<T> entities)
        {
            return Task.FromResult(0);
        }

        public virtual Task DeleteAsync(T entity)
        {
            DbSet.Remove(entity);
            return Task.FromResult(0);
        }

        public virtual Task DeleteRangeAsync(List<T> entities)
        {
            DbSet.RemoveRange(entities);
            return Task.FromResult(0);
        }

        public void CreateRange(List<T> entities)
        {
            DbSet.AddRange(entities);
        }

        public async Task CreateRangeAsync(List<T> entities)
        {
            await DbSet.AddRangeAsync(entities);
        }
    }
}
