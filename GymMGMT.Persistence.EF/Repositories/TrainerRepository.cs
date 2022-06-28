using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymMGMT.Persistence.EF.Repositories
{
    public class TrainerRepository : BaseRepository<Trainer>, ITrainerRepository
    {
        public TrainerRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Trainer>> GetAllWithDetailsAsync()
        {
            var trainers = await _context.Trainers
                .Include(x => x.User)
                .Include(x => x.Trainings)
                .ToListAsync();

            return trainers;
        }

        public async Task<Trainer> GetByIdWithDetailsAsync(int id)
        {
            var trainer = await _context.Trainers
                .Include(x => x.User)
                .Include(x => x.Trainings)
                .FirstOrDefaultAsync(x => x.Id == id);

            return trainer;
        }

        public async Task<Trainer> GetByUserIdWithDetailsAsync(Guid userId)
        {
            var trainer = await _context.Trainers
                .Include(x => x.User)
                .Include(x => x.Trainings)
                .FirstOrDefaultAsync(x => x.UserId == userId);

            return trainer;
        }
    }
}