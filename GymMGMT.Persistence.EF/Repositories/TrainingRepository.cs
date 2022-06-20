using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymMGMT.Persistence.EF.Repositories
{
    public class TrainingRepository : BaseRepository<Training>, ITrainingRepository
    {
        public TrainingRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Training>> GetAllWithDetailsAsync()
        {
            var trainings = await _context.Trainings
                .Include(x => x.Trainer)
                .Include(x => x.Members)
                .ToListAsync();

            return trainings;
        }

        public async Task<Training> GetByIdWithDetailsAsync(int id)
        {
            var training = await _context.Trainings
                .Include(x => x.Trainer)
                .Include(x => x.Members)
                .FirstOrDefaultAsync(x => x.Id == id);

            return training;
        }
    }
}