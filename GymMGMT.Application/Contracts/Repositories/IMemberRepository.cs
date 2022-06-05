using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.Contracts.Repositories
{
    public interface IMemberRepository : IAsyncRepository<Member>
    {
        Task<IReadOnlyList<Member>> GetAllWithDetailsAsync();
        Task<Member> GetByIdWithDetailsAsync(int id);
    }
}