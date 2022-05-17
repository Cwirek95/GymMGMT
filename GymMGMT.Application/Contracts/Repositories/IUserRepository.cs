using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.Contracts.Repositories
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<IReadOnlyList<User>> GetAllWithDetailsAsync();
        Task<User> GetByIdWithDetailsAsync(Guid id);
        Task<User> GetByEmailWithDetailsAsync(string email);
    }
}
