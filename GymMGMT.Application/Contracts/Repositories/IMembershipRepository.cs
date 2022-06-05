using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.Contracts.Repositories
{
    public interface IMembershipRepository : IAsyncRepository<Membership>
    {
        Task<IReadOnlyList<Membership>> GetAllWithDetailsAsync();
        Task<Membership> GetByIdWithDetailsAsync(int id);
    }
}