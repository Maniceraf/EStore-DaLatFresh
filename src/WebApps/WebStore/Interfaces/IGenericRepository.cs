namespace WebStore.Interfaces
{
    public interface IGenericRepository<T>
    {
        T? GetById(int id);
        List<T> GetAll();
        void Create(T entity);
        void CreateRange(List<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(List<T> entities);
        Task<T?> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task CreateAsync(T entity);
        Task CreateRangeAsync(List<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(List<T> entities);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(List<T> entities);
    }
}
