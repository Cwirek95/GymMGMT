namespace GymMGMT.Application.Contracts.Repositories
{
    public interface IAsyncRepository<TEntity> where TEntity : class
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task UpdateListAsync(IEnumerable<TEntity> entities);
        Task DeleteAsync(TEntity entity);
    }
}
