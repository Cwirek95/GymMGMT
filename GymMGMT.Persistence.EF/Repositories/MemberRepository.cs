using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymMGMT.Persistence.EF.Repositories
{
    public class MemberRepository : BaseRepository<Member>, IMemberRepository
    {
        public MemberRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Member>> GetAllWithDetailsAsync()
        {
            var members = await _context.Members
                .Include(x => x.User)
                .Include(x => x.Membership)
                .Include(x => x.Trainings)
                .ToListAsync();

            return members;
        }

        public async Task<Member> GetByIdWithDetailsAsync(int id)
        {
            var member = await _context.Members
                .Include(x => x.User)
                .Include(x => x.Membership)
                .Include(x => x.Trainings)
                .FirstOrDefaultAsync(x => x.Id == id);

            return member;
        }

        public async Task<Member> GetByUserIdWithDetailsAsync(Guid userId)
        {
            var member = await _context.Members
                .Include(x => x.User)
                .Include(x => x.Membership)
                .Include(x => x.Trainings)
                .FirstOrDefaultAsync(x => x.UserId == userId);

            return member;
        }
    }
}