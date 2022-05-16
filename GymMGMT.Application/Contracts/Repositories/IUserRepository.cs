using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.Contracts.Repositories
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<User> GetByIdWithRoleAsync(Guid id);
        Task<User> GetByEmailAsync(string email);
    }
}
