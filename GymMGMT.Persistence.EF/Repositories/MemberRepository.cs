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
                .ToListAsync();

            return members;
        }

        public async Task<Member> GetByIdWithDetailsAsync(int id)
        {
            var member = await _context.Members
                .Include(x => x.User)
                .Include(x => x.Membership)
                .FirstOrDefaultAsync(x => x.Id == id);

            return member;
        }
    }
}