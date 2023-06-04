namespace TheBakery.Data.Repositories
{
    public interface IBakeryRepository<T>
    {
        bool IsNull { get; }
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> FindAsync(Guid id);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Remove(T entity);
        Task SaveAsync();
    }
}
