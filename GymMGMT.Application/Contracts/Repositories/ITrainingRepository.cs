using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.Contracts.Repositories
{
    public interface ITrainingRepository : IAsyncRepository<Training>
    {
        Task<IReadOnlyList<Training>> GetAllWithDetailsAsync();
        Task<Training> GetByIdWithDetailsAsync(int id);
        Task AddMemberAsync(Training training, Member member);
    }
}