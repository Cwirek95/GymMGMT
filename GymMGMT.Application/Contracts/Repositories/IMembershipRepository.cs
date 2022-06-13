using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.Contracts.Repositories
{
    public interface IMembershipRepository : IAsyncRepository<Membership>
    {
        Task<IReadOnlyList<Membership>> GetAllWithDetailsAsync();
        Task<IReadOnlyList<Membership>> GetAllByMembershipTypeIdWithDetailsAsync(int membershipTypeId);
        Task<Membership> GetByIdWithDetailsAsync(int id);
        Task<Membership> GetByMemberIdWithDetailsAsync(int memberId);
    }
}