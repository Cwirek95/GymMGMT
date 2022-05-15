using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Domain.Entities;

namespace GymMGMT.Persistence.EF.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext context) : base(context)
        {
        }
    }
}
