namespace GymMGMT.Application.Contracts.Repositories
{
    public interface IReadOnlyAsyncRepository<TEntity> where TEntity : class
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> GetByIdAsync(Guid id);
    }
}
