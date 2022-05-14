using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.Contracts.Repositories
{
    public interface IUserRepository : IReadOnlyAsyncRepository<User>
    {
    }
}
