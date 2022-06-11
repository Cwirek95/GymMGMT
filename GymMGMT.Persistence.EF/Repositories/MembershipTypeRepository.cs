using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymMGMT.Persistence.EF.Repositories
{
    public class MembershipTypeRepository : BaseRepository<MembershipType>, IMembershipTypeRepository
    {
        public MembershipTypeRepository(AppDbContext context) : base(context)
        {
        }

        public Task<MembershipType> GetByIdWithDetailsAsync(int id)
        {
            var membershipType = _context.MembershipTypes
                .Include(x => x.Memberships)
                .FirstOrDefaultAsync(x => x.Id == id);

            return membershipType;
        }
    }
}