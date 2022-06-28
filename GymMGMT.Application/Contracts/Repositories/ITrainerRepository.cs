using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.Contracts.Repositories
{
    public interface ITrainerRepository : IAsyncRepository<Trainer>
    {
        Task<IReadOnlyList<Trainer>> GetAllWithDetailsAsync();
        Task<Trainer> GetByIdWithDetailsAsync(int id);
        Task<Trainer> GetByUserIdWithDetailsAsync(Guid userId);
    }
}