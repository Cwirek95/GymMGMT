using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.Contracts.Repositories
{
    public interface IMembershipTypeRepository : IAsyncRepository<MembershipType>
    {
        Task<MembershipType> GetByIdWithDetailsAsync(int id);
    }
}