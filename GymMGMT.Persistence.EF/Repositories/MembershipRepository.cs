using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymMGMT.Persistence.EF.Repositories
{
    public class MembershipRepository : BaseRepository<Membership>, IMembershipRepository
    {
        public MembershipRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Membership>> GetAllWithDetailsAsync()
        {
            var memberships = await _context.Memberships
                .Include(x => x.Member)
                .Include(x => x.MembershipType)
                .ToListAsync();

            return memberships;
        }

        public async Task<Membership> GetByIdWithDetailsAsync(int id)
        {
            var membership = await _context.Memberships
                .Include(x => x.Member)
                .Include(x => x.MembershipType)
                .FirstOrDefaultAsync(x => x.Id == id);

            return membership;
        }
    }
}