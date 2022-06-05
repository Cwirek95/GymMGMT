using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Domain.Entities;

namespace GymMGMT.Persistence.EF.Repositories
{
    public class MembershipTypeRepository : BaseRepository<MembershipType>, IMembershipTypeRepository
    {
        public MembershipTypeRepository(AppDbContext context) : base(context)
        {
        }
    }
}